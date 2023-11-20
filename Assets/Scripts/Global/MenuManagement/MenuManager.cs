using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    private Stack<ScreenManager> screenStack = new Stack<ScreenManager>();

    public PauseManager pauseManager;
    public SettingsManager settingsManager;
    private InGameManager inGameManager;
    private TimeManager timeManager;

    private void Awake()
    {
        inGameManager = InGameManager.Instance;
        timeManager = inGameManager.timeManager;
    }

    private void OpenScreen(ScreenManager screenManager)
    {
        if (screenStack.Count > 0)
        {
            screenStack.Peek().DeactivateScreen();
        }
        screenStack.Push(screenManager);
        screenManager.ActivateScreen();
        timeManager.Pause();
    }

    public void GoBack()
    {
        if (screenStack.Count > 0)
        {
            screenStack.Pop().DeactivateScreen();
            if (screenStack.Count > 0)
                screenStack.Peek().ActivateScreen();
        }
        if (screenStack.Count == 0)
        {
            timeManager.Resume();
        }
    }

    public void OpenPause()
    {
        OpenScreen(pauseManager);
    }

    public void OpenSettings()
    {
        OpenScreen(settingsManager);
    }

}