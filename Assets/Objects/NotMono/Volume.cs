
public class Volume
{
    public Volume(float master, float sfx, float music)
    {
        this.master = master;
        this.sfx = sfx;
        this.music = music;
    }
    public float master { get; private set; }
    public float sfx { get; private set; }
    public float music { get; private set; }
}
