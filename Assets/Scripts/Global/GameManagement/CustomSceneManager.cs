using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class CustomSceneManager : MonoBehaviour
{
    public GameObject loadingScreen;   
    public Image loadingSlider;         

    private void Awake()
    {
        if (loadingScreen)
            loadingScreen.SetActive(false);
    }

    public void PlayGame()
    {
        LoadSceneWithLoadingScreen("Wizard");
    }

    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        if (loadingScreen)
            loadingScreen.SetActive(true);

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); 

            if (loadingSlider)
                loadingSlider.fillAmount = progress;

            yield return null;
        }
    }
}
