using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class CustomSceneManager : MonoBehaviour
{
    public GameObject loadingScreen;   
    public Image loadingSlider;  

    private static CustomSceneManager _instance;

    public static CustomSceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CustomSceneManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("CustomSceneManager");
                    _instance = obj.AddComponent<CustomSceneManager>();
                    DontDestroyOnLoad(obj); // Make it persistent
                }
            }
            return _instance;
        }
    }       

    private void Awake()
    {
        if (loadingScreen)
            loadingScreen.SetActive(false);
    }

    public void PlayGame()
    {
        LoadSceneWithLoadingScreen("Wizard");
    }

    // Additions start here
    public void LoadWizardLevelOne()
    {
        LoadSceneWithLoadingScreen("Wizard");
    }

    public void LoadWizardLevelTwo()
    {
        LoadSceneWithLoadingScreen("Wizard-2");
    }

    public void LoadWizardLevelThree()
    {
        LoadSceneWithLoadingScreen("Wizard-3");
    }

    public void LoadCustomLevel(string targetSceneName){
        LoadSceneWithLoadingScreen(targetSceneName);
    }

    public void LoadHUB()
    {
        LoadSceneWithLoadingScreen("HUB");
    }

    public void ReturnToMainMenu()
    {
        LoadSceneWithLoadingScreen("MainMenu");
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

        // Optional: Deactivate the loading screen once the scene is loaded.
        if (loadingScreen)
            loadingScreen.SetActive(false);
    }
}
