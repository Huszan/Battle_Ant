using UnityEngine;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance { get; private set; }
    public GameObject Curtain;
    public GameObject UserBoard;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
    }

}
