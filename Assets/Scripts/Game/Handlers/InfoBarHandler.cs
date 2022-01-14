using TMPro;
using UnityEngine;

public class InfoBarHandler : MonoBehaviour
{
    [Header("Import")]
    public TMP_Text _antCount;
    public TMP_Text _resources;
    public TMP_Text _time;
    public TMP_Text _score;

    private Player player;
    //private GameManager gm;

    private void Start()
    {
        player = 
            GameObject.FindGameObjectWithTag("Human player")
            .GetComponent<Player>();
    }
    private void Update()
    {
        _antCount.text =
            player.UnitCount.ToString() + 
            "/" + 
            player.UnitLimit().ToString();
        _resources.text = player.Resources.ToString();
        _time.text = "THERE WILL BE TIME";
        _score.text = "THERE WILL BE SCORE";
    }
}
