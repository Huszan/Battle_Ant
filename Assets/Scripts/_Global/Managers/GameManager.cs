using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    UNDEFINED = 0,
    LOADING = 1,
    PAUSED = 2,
    PLAYING = 3,
    FINISHED = 4,
}
public enum Difficulty
{
    EASY = 0,
    NORMAL = 1,
    HARD = 2,
    GOD_MODE = 3
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        TimePassed = gameObject.AddComponent<Timer>();
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameState GameState { get; private set; }
    public void SetGameState(GameState gameState)
    {
        GameState = gameState;
        if (gameState == GameState.PAUSED)
            TimePassed.StopCounting();
        if (gameState == GameState.PLAYING)
            TimePassed.StartCounting();
        if (gameState == GameState.FINISHED || gameState == GameState.UNDEFINED)
        {
            TimePassed.StopCounting();
            TimePassed.ResetCounter();
        }
    }
    public Difficulty Difficulty { get; private set; }
    public Timer TimePassed { get; private set; }
    public Player HumanPlayer { get; private set; }
    public List<Player> AiPlayers { get; private set; }
    public List<Player> Players()
    {
        List<Player> list = new List<Player>();
        list.Add(HumanPlayer);
        list.AddRange(AiPlayers);
        return list;
    }

    [Header("End game screen")]
    public GameObject Curtain;
    public GameObject GameWonScreen;
    public GameObject GameLostScreen;
    [Header("Food spawning")]
    [Range(0.05f, 0.50f)]
    public float foodPercentage;
    [Range(3, 10)]
    public int foodDistanceFromEdge;

    public void GenerateNewGame(
        Vector2 mapSize,
        int numberOfOpponents,
        Difficulty difficulty)
    {
        SetGameState(GameState.LOADING);

        Tilemap.Instance.GenerateTilemap(mapSize);
        SpawnFoodSources();
        Tilemap.Instance.GetInfo.Gather();
        Difficulty = difficulty;
        InitializePlayers(numberOfOpponents);
        InitializePlayersAssets();

        SetGameState(GameState.PLAYING);

        AiManager.Instance.enabled = true;
        TimePassed.StartCounting();
    }

    public void LoadGame(SaveData saveData)
    {
        SetGameState(GameState.LOADING);

        Tilemap.Instance.GenerateTilemap(saveData.mapSize);
        SpawnFoodSources();
        Tilemap.Instance.GetInfo.Gather();
        Difficulty = saveData.difficulty;
        LoadPlayers(saveData.sPlayerList);
        LoadPlayerAssets(saveData.sBuildingList);

        SetGameState(GameState.PLAYING);

        AiManager.Instance.enabled = true;
        TimePassed.AddSeconds(saveData.time);
        TimePassed.StartCounting();

        PopupManager.Instance.Pop(PopupManager.PopType.success, "Game loaded successfully");
    }

    public void FinishGame(int index)
    {
        SetGameState(GameState.UNDEFINED);
        AiManager.Instance.enabled = false;
        StartCoroutine(FinishGameAsync(index));
    }

    private IEnumerator FinishGameAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(index);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            Debug.Log("Loading " + operation.progress + "%");
            yield return null;
        }
        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            Debug.Log("Loading " + operation.progress + "%");
            yield return null;
        }

        Scene previousScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(sceneToLoad);
        SceneManager.UnloadSceneAsync(previousScene.buildIndex);
    }

    private void InitializePlayers(int enemiesCount)
    {
        InitializeLocalPlayer();
        InitializeAiPlayers(enemiesCount);
    }
    private void LoadPlayers(List<SPlayer> sPlayerList)
    {
        AiPlayers = new List<Player>();
        for (int i = 0; i < sPlayerList.Count; i++)
        {
            if (i == 0) 
                HumanPlayer = new Player(sPlayerList[i].resources, sPlayerList[i].color);
            else
                AiPlayers.Add(new Player(sPlayerList[i].resources, sPlayerList[i].color));
        }
    }
    private void InitializeLocalPlayer()
    {
        switch (Difficulty)
        {
            case (Difficulty.EASY):
                HumanPlayer = new Player(300f);
                break;
            case (Difficulty.NORMAL):
                HumanPlayer = new Player(200f);
                break;
            case (Difficulty.HARD):
                HumanPlayer = new Player(0f);
                break;
            case (Difficulty.GOD_MODE):
                HumanPlayer = new Player(100000f);
                break;
        }
    }
    private void InitializeAiPlayers(int enemiesCount)
    {
        AiPlayers = new List<Player>();
        for (int i = 0; i < enemiesCount; i++)
            AiPlayers.Add(new Player(200f));
    }
    private void InitializePlayersAssets()
    {
        var corners = Tilemap.Instance.PlayerCorners();
        var mainBuilding = BuildingManager.Instance.buildings[0];

        var i = 0;
        BuildingManager.Instance.PlaceBuilding(
            corners[i++],
            mainBuilding,
            HumanPlayer,
            false);
        foreach (var player in AiPlayers)
        {
            BuildingManager.Instance.PlaceBuilding(
            corners[i++],
            mainBuilding,
            player,
            false);
        }
    }
    private void LoadPlayerAssets(List<SBuilding> sBuildings)
    {
        foreach(SBuilding building in sBuildings)
        {
            if (building.playerId == 0)
            {
                BuildingManager.Instance.PlaceBuilding(
                Tilemap.Instance.CreatedTiles[(int)building.position.x, (int)building.position.y],
                BuildingManager.Instance.buildings[building.id],
                HumanPlayer,
                false);
            }
            else
            {
                BuildingManager.Instance.PlaceBuilding(
                Tilemap.Instance.CreatedTiles[(int)building.position.x, (int)building.position.y],
                BuildingManager.Instance.buildings[building.id],
                AiPlayers[building.playerId-1],
                false);
            }
        }
    }
    private void SpawnFoodSources()
    {
        var tilemap = Tilemap.Instance;
        SpawnFoodInReach();
        SpawnFoodOutOfReach();

        void SpawnFoodInReach()
        {
            foreach (GameObject tile in tilemap.FoodCorners())
            {
                BuildingManager.Instance.PlaceBuilding(
                            tile,
                            BuildingManager.Instance.foodSource,
                            null,
                            false);
            }
        }
        void SpawnFoodOutOfReach()
        {
            int tilesAmount = 0;
            for (int i = foodDistanceFromEdge - 1; i < tilemap.CreatedTiles.GetLength(0) - foodDistanceFromEdge; i++)
                for (int j = foodDistanceFromEdge; j < tilemap.CreatedTiles.GetLength(1) - foodDistanceFromEdge; j++)
                    tilesAmount++;
            int generateEvery = tilesAmount / (int)(tilesAmount * foodPercentage);

            int count = 0;
            for (int i = foodDistanceFromEdge - 1; i < tilemap.CreatedTiles.GetLength(0) - foodDistanceFromEdge; i++)
            {
                for (int j = foodDistanceFromEdge; j < tilemap.CreatedTiles.GetLength(1) - foodDistanceFromEdge; j++)
                {
                    count++;
                    if (count == generateEvery)
                    {
                        BuildingManager.Instance.PlaceBuilding(
                            tilemap.CreatedTiles[i, j],
                            BuildingManager.Instance.foodSource,
                            null,
                            false);
                        count = 0;
                    }
                }
            }
        }
    }

    public void ClearFromGame(Player player)
    {
        for (int i = player.Segregator.GetBuildings.Count-1; i >= 0; i--)
            {
                Building building = player.Segregator.GetBuildings[i];
                player.Segregator.RemoveBuilding(building);
                Destroy(building.gameObject);
            }
                
        if (AiPlayers.Contains(player))
            AiPlayers.Remove(player);
    }
    public void CheckEndgameCoditions()
    {
        if (HumanPlayer.IsDefeated())
        {
            SetGameState(GameState.FINISHED);
            Curtain.SetActive(true);
            GameLostScreen.SetActive(true);
        }
        else if (AiPlayers.Count <= 0)
        {
            SetGameState(GameState.FINISHED);
            Curtain.SetActive(true);
            GameWonScreen.SetActive(true);
        }
    }

}