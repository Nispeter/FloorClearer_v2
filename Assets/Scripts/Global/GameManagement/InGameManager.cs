using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private static InGameManager _instance;

    public static InGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InGameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("InGameManager");
                    _instance = obj.AddComponent<InGameManager>();
                }
            }
            return _instance;
        }
    }

    public SpellUI spellUI;
    public HealthBar healthBar;
    public PointCounter pointCounter;
    public TimeManager timeManager;
    public InputController inputController;

    private void Awake()
    {
        if (timeManager == null)
        {
            GameObject timeObj = new GameObject("TimeManager");
            timeManager = timeObj.AddComponent<TimeManager>();
        }
        timeManager.Resume();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        ActivateGameUI();
        inputController.LockCursor();
    }

    public GameObject gameUI;
    public GameObject pauseScreen;
    public GameObject settingsScreen;
    public GameObject pixelFilter;
    

    public void PauseGame()
    {
        timeManager.Pause();
    }

    public void ResumeGame()
    {
        timeManager.Resume();
    }

    public void OpenSettingsScreen()
    {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void CloseSettingsScreen()
    {
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void OpenPauseScreen()
    {
        DeactivateGameUI();
        PauseGame();
        inputController.UnlockCursor();
    }

    public void ExitPauseScreen()
    {
        ActivateGameUI();
        ResumeGame();
        inputController.LockCursor();
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
        ResumeGame();
    }

    private void ActivateGameUI()
    {
        pixelFilter.SetActive(true);
        gameUI.SetActive(true);
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    private void DeactivateGameUI()
    {
        pixelFilter.SetActive(false);
        gameUI.SetActive(false);
        pauseScreen.SetActive(true);
    }

}
