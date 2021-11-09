using UnityEngine;
using UnityEngine.UI;

public class StartHandler : MonoBehaviour
{
    [Header("Form")]
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderEnemies;
    public TMPro.TMP_Dropdown dropdownDifficulty;
    [Header("Game generation")]
    public GameObject tilemapPrefab;
    [Header("Transition")]
    public GameObject[] objectsToMove;

    public Vector2 MapSize()
    {
        return new Vector2(sliderX.value, sliderY.value);
    }

    public void StartGame(int index)
    {
        if(GameObject.Find("Tilemap") == null)
        {
            GameObject generatedTilemap = Instantiate(tilemapPrefab);
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
