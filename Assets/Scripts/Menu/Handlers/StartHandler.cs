using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private Vector2 GetMapSize() =>
        new Vector2(sliderX.value, sliderY.value);
    private int GetNumberOfOponents() =>
        (int)sliderEnemies.value;
    private Difficulty GetDifficulty() =>
        (Difficulty)dropdownDifficulty.value;

    public void StartNewGame(int index)
    {
        StartCoroutine(StartGameAsync(index));
    }

    private IEnumerator StartGameAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(index);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            Debug.Log("Loading " + operation.progress + "%");
            yield return null;
        }
        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            Debug.Log("Loading " + operation.progress + "%");
            yield return null;
        }

        Scene previousScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(sceneToLoad);
        SceneManager.UnloadSceneAsync(previousScene.buildIndex);
        GameManager.Instance.GenerateNewGame(
            GetMapSize(),
            GetNumberOfOponents(),
            GetDifficulty());
    }

}
