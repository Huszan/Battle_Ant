using UnityEngine;

public class AiManager : MonoBehaviour
{
    public static AiManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        //AddAiToHumanPlayer();
        AddAiToComputerPlayers();
    }
    private void AddAiToHumanPlayer()
    {
        GameManager.Instance.HumanPlayer.AddAi();
        StartCoroutine(GameManager.Instance.HumanPlayer.PlayerAi.Process());
    }
    private void AddAiToComputerPlayers()
    {
        foreach (Player player in GameManager.Instance.AiPlayers)
            {
                player.AddAi();
                StartCoroutine(player.PlayerAi.Process());
            }
    }
}
