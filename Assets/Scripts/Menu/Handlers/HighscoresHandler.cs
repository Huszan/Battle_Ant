
using UnityEngine;

public class HighscoresHandler : MonoBehaviour
{
    public GameObject view;
    public GameObject hsBlock;

    public void LoadHighscores()
    {
        RemoveOldHighscores();

        var highscores = DatabaseManager.GetAllHighscores();
        var index = 1;
        foreach (Highscore hs in highscores)
        {
            var block = Instantiate(hsBlock, view.transform).GetComponent<HighscoreBlock>();
            block.Index = index++;
            block.Hs = hs;

            ((RectTransform)view.transform).sizeDelta += new Vector2(0, 
                ((RectTransform)block.transform).sizeDelta.y);
        }

    }

    private void RemoveOldHighscores()
    {
        ((RectTransform)view.transform).sizeDelta = new Vector2(0, 0);
        foreach (Transform transform in view.transform)
        {
            Destroy(transform.gameObject);
        }
    }

}
