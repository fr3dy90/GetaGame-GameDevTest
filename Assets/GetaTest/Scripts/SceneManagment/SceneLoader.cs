using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class SceneLoader : MonoBehaviour
{
    public Image coverBaar;
    public bool useTrueLoad;


    private void Start()
    {
        LoadLevel(GlobalData.indexScene);
    }

    public void LoadLevel(int sceneIndex)
    {
        if (useTrueLoad)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }
        else
        {
            StartCoroutine(Fakeload(sceneIndex));
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            coverBaar.fillAmount = progress;
            yield return null;
        }
    }

    IEnumerator Fakeload(int sceneIndex)
    {
        float progress = 3f;
        float loadProgress = 0;
        while (loadProgress < progress)
        {
            yield return null;
            loadProgress += Time.deltaTime;
            coverBaar.fillAmount =  loadProgress/progress;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    }
}