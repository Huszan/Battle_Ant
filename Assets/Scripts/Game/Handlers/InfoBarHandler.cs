using TMPro;
using UnityEngine;

public class InfoBarHandler : MonoBehaviour
{
    [Header("Import")]
    public TMP_Text _antCount;
    public TMP_Text _resources;
    public TMP_Text _time;
    public TMP_Text _score;

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.LOADING && 
            GameManager.Instance.GameState != GameState.UNDEFINED)
        {
            Player player = GameManager.Instance.Players[0];
            Timer timer = GameManager.Instance.TimePassed;

            _antCount.text =
                player.UnitCount.ToString() +
                "/" +
                player.UnitLimit().ToString();
            _resources.text = player.Resources.ToString();
            _time.text = TimeFormatter.GetFullTime(timer.Counter);
            _score.text = player.Score.ToString();
        }
    }
}
