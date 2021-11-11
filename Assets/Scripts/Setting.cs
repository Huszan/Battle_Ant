
using UnityEngine;

public class Setting
{
    public Volume volume { get; set; }
    public Resolution resolution { get; set; }
    public bool fullscreen { get; set; }

    public void Log()
    {
        Debug.Log(
            "Setting setup : " +
            "\nresolution : " + resolution.ToString() +
            "\nfullscreen : " + fullscreen.ToString() +
            "\nvolume master : " + volume.master.ToString() +
            "\nvolume music : " + volume.music.ToString() +
            "\nvolume sfx : " + volume.sfx.ToString());
    }
}
