using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    UNDEFINED = 0,
    LOADING = 1,
    PAUSED = 2,
    PLAYING = 3
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
            Difficulty.EASY);
    }

    public GameState GameState { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public Timer TimePassed { get; private set; }
    public List<Player> Players { get; private set; }
    public Tilemap Tilemap { get; private set; }

    public void GenerateNewGame(
        Vector2 mapSize,
        int numberOfOpponents,
        Difficulty difficulty)
    {
        GameState = GameState.LOADING;

        InitializePlayers(numberOfOpponents, difficulty);

        Tilemap = FindTilemap();
        Tilemap.GenerateTilemap(mapSize);

        InitializePlayersAssets();
        // TODO -> Spawn food sources

        GameState = GameState.PLAYING;

        CameraController.Instance.Toggle();
        TimePassed.StartCounting();
    }

    private void InitializePlayers(int numberOfOpponents, Difficulty difficulty)
    {
        Players = new List<Player>();
        Difficulty = difficulty;
        switch(difficulty)
        {
            case Difficulty.EASY:
                {
                    Players.Add(new Player(200, 0, 1000));
                    break;
                }
            case Difficulty.NORMAL:
                {
                    Players.Add(new Player(100, 0, 2000));
                    break;
                }
            case Difficulty.HARD:
                {
                    Players.Add(new Player(30, 0, 3000));
                    break;
                }
        }

        for (int i = 0; i < numberOfOpponents; i++)
            Players.Add(
                new Player(100, 0, 0)
                );
        Debug.Log($"Generated {Players.Count} players");
    }
    private void InitializePlayersAssets()
    {
        var corners = Tilemap.GetCorners();
        var mainBuilding = BuildingManager.Instance.buildings[0];

        var i = 0;
        foreach (var player in Players)
        {
            BuildingManager.Instance.PlaceBuilding(
            corners[i++],
            mainBuilding,
            player);
        }
    }
    private Tilemap FindTilemap()
    {
        GameObject tm = GameObject.FindGameObjectWithTag("Tilemap");
        if (tm == null)
        {
            GameObject _Tilemap = new GameObject
            {
                name = "_Tilemap",
                tag = "tilemap"
            };
            _Tilemap.AddComponent<Tilemap>();
            Debug.Log("Tilemap was created, because it did't exist");
        }
        return tm.GetComponent<Tilemap>();
    }

}
