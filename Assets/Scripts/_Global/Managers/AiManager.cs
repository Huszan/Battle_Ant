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
        //GameManager.Instance.HumanPlayer.AddAi();
        //StartCoroutine(GameManager.Instance.HumanPlayer.PlayerAi.Process());
        foreach (Player player in GameManager.Instance.AiPlayers)
        {
            player.AddAi();
            StartCoroutine(player.PlayerAi.Process());
        }
    }
}
