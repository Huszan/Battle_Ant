using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; set; }
    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject ingameMenu;
    public GameObject curtain;

    void Update()
    {
        if (GameManager.Instance.GameState != GameState.UNDEFINED)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ToggleIngameMenu();
        }
    }

    public void ToggleIngameMenu()
    {
        ingameMenu.SetActive(!ingameMenu.activeSelf);
        curtain.SetActive(!curtain.activeSelf);
        if (GameManager.Instance.GameState == GameState.PLAYING)
            GameManager.Instance.SetGameState(GameState.PAUSED);
        else
            GameManager.Instance.SetGameState(GameState.PLAYING);
    }

}
