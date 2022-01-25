
public abstract class JobBase
{
    public abstract bool Finished { get; set; }
    public abstract void Perform();
    public abstract void Conclude();
}
