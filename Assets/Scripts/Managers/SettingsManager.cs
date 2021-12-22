
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }
    private Setting setting;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void SaveSetting(Setting setting)
    {
        this.setting = setting;
        UpdateSettings();

        setting.Log();
        Debug.Log(Screen.currentResolution);
        PopupManager.Instance.Pop(
            PopupManager.PopType.success, 
            "Settings changed");
    }
    private void UpdateSettings()
    {
        // THERE WILL BE CODE FOR SOUND UPDATE
        Screen.SetResolution(
            setting.screenWidth,
            setting.screenHeight,
            setting.fullscreen,
            setting.refreshRate);
    }

}
