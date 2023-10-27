using UnityEngine;

public class MMButtonManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public CustomSceneManager sceneManager; 

    private void Start()
    {
        if (mainMenu)
            mainMenu.SetActive(true);

        if (settings)
            settings.SetActive(false);
    }

    public void StartGame()
    {
        if (sceneManager)
            sceneManager.PlayGame();
    }

    public void ToggleSettings()
    {
        if (!mainMenu || !settings) return;

        bool isMainMenuActive = mainMenu.activeSelf;

        mainMenu.SetActive(!isMainMenuActive);
        settings.SetActive(isMainMenuActive);
    }

    public void GoBack()
    {
        if (settings && settings.activeSelf)
        {
            settings.SetActive(false);
        }

        if (mainMenu)
        {
            mainMenu.SetActive(true);
        }
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
