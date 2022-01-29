using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserBlock : MonoBehaviour
{
    public User user { get; set; }
    [Header("References")]
    [SerializeField]
    private TMP_Text usernameText;

    public Button selectButton;
    public Button deleteButton;

    private void Awake()
    {
        if (user != null)
            usernameText.text = user.name;
        else usernameText.text = "INVALID";
    }
}
