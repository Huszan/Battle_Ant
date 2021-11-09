using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager
{
    public static IEnumerator LoadScene(int index, GameObject[] objectsToMove = null)
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

        
        if (!(objectsToMove is null))
            foreach (GameObject go in objectsToMove) 
                SceneManager.MoveGameObjectToScene(GameObject.Find(go.name), sceneToLoad);

        while (!operation.isDone)
        {
            Debug.Log("Loading " + operation.progress + "%");
            yield return null;
        }

        Scene previousScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(sceneToLoad);
        SceneManager.UnloadSceneAsync(previousScene.buildIndex);
    }
}
