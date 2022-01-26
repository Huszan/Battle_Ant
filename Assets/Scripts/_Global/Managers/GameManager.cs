using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    UNDEFINED = 0,
    LOADING = 1,
    PAUSED = 2,
    PLAYING = 3,
}
public enum Difficulty
{
    EASY,
    NORMAL,
    HARD,
    GOD_MODE
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

    /* DELETE ME AFTER DEVELOPMENT!!!*/
    private void Start()
    {
        GenerateNewGame(
            new Vector2(30, 30),
            1,
            Difficulty.GOD_MODE);
        Debug.Log($"Map size -> {Tilemap.Instance.MapSize}\n" +
            $"List size -> {Tilemap.Instance.CreatedTiles.GetLength(0)},{Tilemap.Instance.CreatedTiles.GetLength(1)}");
    }

    public GameState GameState { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public Timer TimePassed { get; private set; }
    public List<Player> Players { get; private set; }
    public Player HumanPlayer => Players[0];

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
        GameState = GameState.LOADING;

        Tilemap.Instance.GenerateTilemap(mapSize);
        InitializePlayers(numberOfOpponents, difficulty);
        InitializePlayersAssets();
        SpawnFoodSources();

        GameState = GameState.PLAYING;

        CameraController.Instance.Toggle();
        TimePassed.StartCounting();
    }

    private void InitializePlayers(int enemiesCount, Difficulty difficulty)
    {
        Players = new List<Player>();
        InitializeLocalPlayer(difficulty);
        InitializeAiPlayers(enemiesCount);
    }
    private void InitializeLocalPlayer(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case (Difficulty.EASY):
                Players.Add(new Player(300f));
                break;
            case (Difficulty.NORMAL):
                Players.Add(new Player(200f));
                break;
            case (Difficulty.HARD):
                Players.Add(new Player(0f));
                break;
            case (Difficulty.GOD_MODE):
                Players.Add(new Player(100000f));
                break;
        }
    }
    private void InitializeAiPlayers(int enemiesCount)
    {
        for (int i = 0; i < enemiesCount; i++)
            Players.Add(new Player(200f));
    }

    private void InitializePlayersAssets()
    {
        var corners = Tilemap.Instance.PlayerCorners();
        var mainBuilding = BuildingManager.Instance.buildings[0];

        var i = 0;
        foreach (var player in Players)
        {
            BuildingManager.Instance.PlaceBuilding(
            corners[i++],
            mainBuilding,
            player,
            false);
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

}