using TMPro;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [Header("References")]
    public TMP_Text productNameField;
    public TMP_Text userNameField;
    public BackgroundAnimator backgroundAnimator;

    public void Start()
    {
        productNameField.text = Application.productName;
        backgroundAnimator.Toggle();
    }

    public void Update()
    {
        if (CurrentUser.user != null)
            userNameField.text = CurrentUser.user.name;
    }

}
