using UnityEngine;

[CreateAssetMenu(
    fileName = "Popup", 
    menuName = "ScriptableObjects/Popup", 
    order = 1)]

public class PopupData : ScriptableObject
{
    public Color boxColor;
    public Color fontColor;
    public float timeToDestroy;
}
