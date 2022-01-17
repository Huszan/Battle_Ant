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

    /* DELETE ME AFTER DEVELOPMENT!!!
    private void Start()
    {
        GenerateNewGame(
            new Vector2(30, 30),
            1,
            Difficulty.EASY);
    }*/

    public GameState GameState { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public Timer TimePassed { get; private set; }
    public Player HumanPlayer { get; private set; }
    public List<Player> AiPlayers { get; private set; }
    public Tilemap Tilemap { get; private set; }

    public void GenerateNewGame(
        Vector2 mapSize,
        int numberOfOpponents,
        Difficulty difficulty)
    {
        GameState = GameState.LOADING;
        InitializePlayers(numberOfOpponents, difficulty);
        // TODO -> Give players initial buildings

        Tilemap = FindTilemap();
        Tilemap.GenerateTilemap(mapSize);
        GameState = GameState.PLAYING;
        CameraController.Instance.Toggle();

        TimePassed.StartCounting();
    }

    private void InitializePlayers(int numberOfOpponents, Difficulty difficulty)
    {
        Difficulty = difficulty;
        switch(difficulty)
        {
            case Difficulty.EASY:
                {
                    HumanPlayer = new Player(200, 0, 1000);
                    break;
                }
            case Difficulty.NORMAL:
                {
                    HumanPlayer = new Player(100, 0, 2000);
                    break;
                }
            case Difficulty.HARD:
                {
                    HumanPlayer = new Player(30, 0, 3000);
                    break;
                }
        }

        AiPlayers = new List<Player>();
        for (int i = 0; i < numberOfOpponents; i++)
            AiPlayers.Add(
                new Player(100, 0, 0)
                );
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
