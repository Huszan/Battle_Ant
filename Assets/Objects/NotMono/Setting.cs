
using UnityEngine;

public class Setting
{
    public Volume volume { get; set; }
    public int screenWidth { get; private set; }
    public int screenHeight { get; private set; }
    public int refreshRate { get; private set; }
    public bool fullscreen { get; private set; }

    public string GetResolution()
    {
        return screenWidth.ToString() + "x"
            + screenHeight.ToString()
            + "@" + refreshRate.ToString();
    }
    public void SetResolution(int width, int height, bool fullscreen, int refreshRate)
    {
        screenWidth = width;
        screenHeight = height;
        this.fullscreen = fullscreen;
        this.refreshRate = refreshRate;
    }
    public void Log()
    {
        Debug.Log(
            "Setting setup : " +
            "\nresolution : " + GetResolution() +
            "\nfullscreen : " + fullscreen.ToString() +
            "\nvolume master : " + volume.master.ToString() +
            "\nvolume music : " + volume.music.ToString() +
            "\nvolume sfx : " + volume.sfx.ToString());
    }
}
