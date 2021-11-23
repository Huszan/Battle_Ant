using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private Image popupImage;
    [SerializeField] private DestroyObject destroyer;

    public void Setup(PopupData popupData, string message)
    {
        popupText.text = message;
        popupText.color = popupData.fontColor;
        popupImage.color = popupData.boxColor;
        destroyer.timeToDestroy = popupData.timeToDestroy;
    }
}
