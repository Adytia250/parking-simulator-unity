using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public static int nextSceneIndex;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(8f);

        AsyncOperation operation =
            SceneManager.LoadSceneAsync(
                nextSceneIndex
            );

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}