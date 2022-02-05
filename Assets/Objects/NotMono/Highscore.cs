
public class Highscore
{

    public int Score { get; private set; }
    public int Uid { get; private set; }

    public Highscore(int score, int uid)
    {
        Score = score;
        Uid = uid;
    }

}
