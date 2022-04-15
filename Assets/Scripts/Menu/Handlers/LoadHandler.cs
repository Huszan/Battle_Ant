using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHandler : MonoBehaviour
{
    public void LoadGame(int index)
    {
        var saveData = SaveSystem.Instance.LoadDataFromFile("SaveFile");
        if (saveData == null)
        {
            PopupManager.Instance.Pop(
                PopupManager.PopType.warning, 
                "There is no save file available on this profile. Try starting a new game!");
            return;
        }
        StartCoroutine(LoadGameAsync(index, saveData));
    }

    private IEnumerator LoadGameAsync(int index, SaveData saveData)
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
        GameManager.Instance.LoadGame(saveData);
    }

}
