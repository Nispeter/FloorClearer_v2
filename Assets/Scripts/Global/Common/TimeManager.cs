using UnityEngine;
using TMPro; 

public class TimeManager : MonoBehaviour
{
    public bool isGamePaused = false;
    public float gameTime = 300; 

    public TextMeshProUGUI timeText;

    private void Update()
    {
        if (!isGamePaused)
        {
            gameTime -= Time.deltaTime;
            UpdateTimeDisplay();
        }

        if (gameTime <= 0)
        {
            gameTime = 0;
            isGamePaused = true;
            InGameManager.Instance.TimeUp();
        }
    }

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

    private void UpdateTimeDisplay()
    {
        if (timeText != null)
        {
            timeText.text = FormatTime(gameTime);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
