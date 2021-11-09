using UnityEngine;
public class PopupManager : MonoBehaviour
{
    [Header("Data")]
    public PopupData successData;
    public PopupData warningData;
    public PopupData errorData;
    public PopupData infoData;
    [Header("References")]
    [SerializeField]
    private GameObject popupBoxPrefab;
    [SerializeField]
    private GameObject popupFrame;

    public void PopSuccess(string message)
    {
        GameObject pop = Instantiate(popupBoxPrefab, popupFrame.transform);
        pop.GetComponent<Popup>().Setup(successData, message);
        MovePops();
    }
    public void PopWarning(string message)
    {
        GameObject pop = Instantiate(popupBoxPrefab, popupFrame.transform);
        pop.GetComponent<Popup>().Setup(warningData, message);
        MovePops();
    }
    public void PopError(string message)
    {
        GameObject pop = Instantiate(popupBoxPrefab, popupFrame.transform);
        pop.GetComponent<Popup>().Setup(errorData, message);
        MovePops();
    }
    public void PopInfo(string message)
    {
        GameObject pop = Instantiate(popupBoxPrefab, popupFrame.transform);
        pop.GetComponent<Popup>().Setup(infoData, message);
        MovePops();
    }

    private void MovePops()
    {
        foreach (Transform child in popupFrame.transform)
        {
            child.position += new Vector3(0, -100, 0);
        }
    }

}