using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{

    public GameObject popupErrorPrefab;
    public GameObject popupInfoPrefab;

    public static void PopError(string text)
    {
        Debug.LogError(text);
    }
    public static void PopInfo(string text)
    {
        Debug.Log(text);
    }

}