using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool isGamePaused = false;
    public void Pause()
    {
        isGamePaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }
}
