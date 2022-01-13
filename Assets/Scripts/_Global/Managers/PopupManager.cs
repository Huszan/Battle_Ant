using UnityEngine;
public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }
    public enum PopType
    {
        success,
        warning,
        error,
        info
    }

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

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Pop(PopType type, string message)
    {
        GameObject pop = Instantiate(popupBoxPrefab, popupFrame.transform);
        switch(type)
        {
            case PopType.success:
                pop.GetComponent<Popup>().Setup(successData, message);
                break;
            case PopType.warning:
                pop.GetComponent<Popup>().Setup(warningData, message);
                break;
            case PopType.error:
                pop.GetComponent<Popup>().Setup(errorData, message);
                break;
            case PopType.info:
                pop.GetComponent<Popup>().Setup(infoData, message);
                break;
        }
    }

}