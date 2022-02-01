
using TMPro;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    private GameManager Gm { get; set; }
    private Highscore CurrentGameScore()
    {
        int? uid = DatabaseManager.GetUidByName(CurrentUser.user.name);
        int score = Gm.HumanPlayer.Score();

        if (uid != null)
            return new Highscore(score, (int)uid);
        else
            return null;
    }
    private Highscore ScoreToSave
    {
        get; set;
    }
    private void OnEnable()
    {
        Gm = GameManager.Instance;
        ScoreToSave = CurrentGameScore();
        ScoreText.text = Gm.HumanPlayer.Score().ToString();
    }

    [SerializeField]
    public TMP_Text ScoreText;

    public void SaveScore()
    {
        if (ScoreToSave != null)
            DatabaseManager.AddHighscore(ScoreToSave);
    }

}
