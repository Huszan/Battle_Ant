using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{

    public TMPro.TMP_Text gameTitle;

    public void Start()
    {
        SetGameTitle(Application.productName);
    }
    private void SetGameTitle(string title)
    {
        gameTitle.text = title;
    }

}
