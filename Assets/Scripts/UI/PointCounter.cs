using TMPro;
using UnityEngine;

public class PointCounter : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI pointsText;

    private int currentPoints = 0;

    void Start(){
        UpdatePointsText();
    }
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

    public void LoadData(GameData data)
    {
        if(data.pointCount != null){
            currentPoints = data.pointCount;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.pointCount =  currentPoints;
    }
}
