using TMPro;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    private int currentPoints = 0;

    public int Points
    {
        get => currentPoints;
        set
        {
            currentPoints = value;
            UpdatePointsText();
        }
    }

    private void UpdatePointsText()
    {
        pointsText.text = $"Points: {currentPoints}";
    }

    public void AddPoints(int pointsToAdd)
    {
        Points += pointsToAdd;
    }
}
