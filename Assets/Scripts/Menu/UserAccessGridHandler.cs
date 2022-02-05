using UnityEngine;

public class UserAccessGridHandler : MonoBehaviour
{
    public GameObject Curtain { get; private set; }
    public GameObject UserBoard { get; private set; }

    public void Activate()
    {
        Curtain = MainUI.Instance.Curtain;
        UserBoard = MainUI.Instance.UserBoard;

        Curtain.SetActive(true);
        UserBoard.SetActive(true);
    }
}
