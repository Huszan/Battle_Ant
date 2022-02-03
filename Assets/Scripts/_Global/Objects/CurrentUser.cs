using UnityEngine;

public static class CurrentUser
{
    public static User user { get; set; }

    public static void ExecuteSettings()
    {
        SettingsManager.Instance.sfxHandler.source.volume = (user.setting.volume.sfx / 100) * (user.setting.volume.master / 100);
        SettingsManager.Instance.musicHandler.source.volume = (user.setting.volume.music / 100) * (user.setting.volume.master / 100);
        Screen.SetResolution(
            user.setting.screenWidth,
            user.setting.screenHeight,
            user.setting.fullscreen,
            user.setting.refreshRate);
    }
}
