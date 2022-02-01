using UnityEngine;

public static class CurrentUser
{
    public static User user { get; set; }

    public static void ExecuteSettings()
    {
        Screen.SetResolution(
            user.setting.screenWidth,
            user.setting.screenHeight,
            user.setting.fullscreen,
            user.setting.refreshRate);
    }
}
