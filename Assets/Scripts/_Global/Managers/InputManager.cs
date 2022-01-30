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
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.GameState == GameState.PLAYING)
            ToggleIngameMenu();
    }

    private void ToggleIngameMenu()
    {
        ingameMenu.SetActive(!ingameMenu.activeSelf);
        curtain.SetActive(!curtain.activeSelf);
    }

}
