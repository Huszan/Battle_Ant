
using TMPro;
using UnityEngine;

public class HighscoreBlock : MonoBehaviour
{
    public int Index { get; set; }
    public Highscore Hs { get; set; }
    [Header("References")]
    [SerializeField]
    private TMP_Text indexText;
    [SerializeField]
    private TMP_Text usernameText;
    [SerializeField]
    private TMP_Text scoreText;

    private void Start()
    {
        var username = DatabaseManager.GetUsername(Hs.Uid);
        if (username != null)
            usernameText.text = username;
        else usernameText.text = "Ant ghost";
        scoreText.text = Hs.Score.ToString();
        indexText.text = Index.ToString() + ".";
    }
}
