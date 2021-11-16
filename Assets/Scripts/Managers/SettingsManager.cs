
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private PopupManager popupManager;
    private Setting setting;

    private void Awake()
    {
        popupManager = GameObject.Find("GlobalManagers")
            .GetComponent<PopupManager>();
    }
    public void SaveSetting(Setting setting)
    {
        this.setting = setting;
        UpdateSettings();

        setting.Log();
        Debug.Log(Screen.currentResolution);
        popupManager.PopSuccess("Settings changed");
    }
    private void UpdateSettings()
    {
        // THERE WILL BE CODE FOR SOUND UPDATE
        Screen.SetResolution(
            setting.resolution.width,
            setting.resolution.height,
            setting.fullscreen,
            setting.resolution.refreshRate);
    }

}
