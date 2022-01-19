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
            Difficulty.GOD_MODE);
    }

    public GameState GameState { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public Timer TimePassed { get; private set; }
    public List<Player> Players { get; private set; }

    public void GenerateNewGame(
        Vector2 mapSize,
        int numberOfOpponents,
        Difficulty difficulty)
    {
        GameState = GameState.LOADING;

        Tilemap.Instance.GenerateTilemap(mapSize);
        InitializePlayers(numberOfOpponents, difficulty);
        InitializePlayersAssets();
        // TODO -> Spawn food sources

        GameState = GameState.PLAYING;

        CameraController.Instance.Toggle();
        TimePassed.StartCounting();
    }

    private void InitializePlayers(int numberOfOpponents, Difficulty difficulty)
    {
        Players = new List<Player>();
        for (int i = 0; i < numberOfOpponents + 1; i++)
            Players.Add(new Player((int)difficulty));
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

}
