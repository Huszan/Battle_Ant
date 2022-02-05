
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }
    [Header("Audio sources")]
    public SfxHandler sfxHandler;
    public MusicHandler musicHandler;

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
    public void UpdateSettings()
    {
        sfxHandler.source.volume = (setting.volume.sfx / 100) * (setting.volume.master / 100);
        musicHandler.source.volume = (setting.volume.music / 100) * (setting.volume.master / 100);
        Screen.SetResolution(
            setting.screenWidth,
            setting.screenHeight,
            setting.fullscreen,
            setting.refreshRate);
    }

}
