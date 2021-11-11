
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private Setting setting;

    public void SaveSetting(Setting setting)
    {
        this.setting = setting;
        UpdateSettings();

        setting.Log();
        Debug.Log(Screen.currentResolution);
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
