using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartHandler : MonoBehaviour
{
    [Header("Form")]
    [SerializeField]
    private Slider sliderX;
    [SerializeField]
    private Slider sliderY;
    [SerializeField]
    private Slider sliderEnemies;
    [SerializeField]
    private TMP_Dropdown dropdownDifficulty;

    [Header("Game generation")]
    [SerializeField]
    private GameObject tilemapGenerator;

    [Header("Transition")]
    [SerializeField]
    private GameObject[] objectsToMove;

    public Vector2 MapSize()
    {
        return new Vector2(sliderX.value, sliderY.value);
    }

    public void StartGame(int index)
    {
        if(GameObject.Find("Tilemap") == null)
        {
            GameObject generatedTilemap = Instantiate(tilemapGenerator);
            generatedTilemap.name = "Tilemap";
            generatedTilemap.GetComponent<Tilemap>().GenerateTilemap(MapSize());
        }
        else
        {
            Debug.LogError("Map is already generated");
        }

        StartCoroutine(SceneTransitionManager.LoadScene(index, objectsToMove));
    }

}
