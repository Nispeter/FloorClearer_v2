using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private static InGameManager _instance;

    public GameObject gameUI;
    public GameObject pixelFilter;
    public MenuManager menuManager;

    public SpellUI spellUI;
    public HealthBar healthBar;
    public PointCounter pointCounter;
    public TimeManager timeManager;
    public InputController inputController;

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

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        if (timeManager == null)
        {
            GameObject timeObj = new GameObject("TimeManager");
            timeManager = timeObj.AddComponent<TimeManager>();
        }
        ActivateGameUI();
        timeManager.Resume();
    }

    public void OpenPauseScreen()
    {
        inputController.UnlockCursor();
        DeactivateGameUI();
        menuManager.OpenPause();
    }   

    public void ExitPauseScreen()
    {
        inputController.LockCursor();
        ActivateGameUI();
        menuManager.GoBack(); 
    }

    private void ActivateGameUI()
    {
        pixelFilter.SetActive(true);
        gameUI.SetActive(true);   
    }

    private void DeactivateGameUI()
    {
        pixelFilter.SetActive(false);
        gameUI.SetActive(false);
    }

}
