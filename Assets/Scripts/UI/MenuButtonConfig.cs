using TMPro;
using UnityEngine;

public class MenuButtonConfig : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpComponent;
    public string buttonText;

    private void Start()
    {
        SetButtonText();
    }

    public void SetButtonText()
    {
        if (tmpComponent != null)
        {
            tmpComponent.text = buttonText;
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI component not assigned in MenuButtonConfig on " + gameObject.name);
        }
    }

    public void UpdateButtonText(string newText)
    {
        buttonText = newText;
        SetButtonText();
    }
}
