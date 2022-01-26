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

    public GameObject miniGameMenu;
    public GameObject curtain;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.GameState == GameState.PLAYING)
            ToggleMiniGameMenu();
    }

    private void ToggleMiniGameMenu()
    {
        miniGameMenu.SetActive(!miniGameMenu.activeSelf);
        curtain.SetActive(!curtain.activeSelf);
    }

}
