using UnityEngine;
using TMPro;

public class BackgroundHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TMP_Text productNameField;
    [SerializeField]
    private BackgroundAnimator backgroundAnimator;

    public void Start()
    {
        productNameField.text = Application.productName;
        backgroundAnimator.Toggle();
    }

}
