using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Player HumanPlayer { get; private set; }
    public List<Player> AiPlayers { get; private set; }
    public Tilemap Tilemap { get; private set; }

    public void GenerateNewGame(
        Vector2 mapSize,
        int numberOfOpponents,
        Difficulty difficulty)
    {
        Tilemap = FindTilemap();
        Tilemap.GenerateTilemap(mapSize);
        CameraController.Instance.Toggle();
        // TODO -> generate player and opponents based on difficulty
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
