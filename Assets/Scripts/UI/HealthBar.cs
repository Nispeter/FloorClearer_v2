using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;

    private void Awake()
    {
    }

    public void UpdateHealthBar(float remainingHealth,float health)
    {
        healthBarImage.fillAmount = remainingHealth / health;
    }
}
