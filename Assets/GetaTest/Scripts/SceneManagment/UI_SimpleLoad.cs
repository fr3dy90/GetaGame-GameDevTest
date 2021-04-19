using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_SimpleLoad : MonoBehaviour
{
    public void SceneLoading(int indexScene)
    {
        GlobalData.indexScene = indexScene;
        SceneManager.LoadScene(1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
