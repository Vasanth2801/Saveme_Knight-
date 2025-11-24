using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider playerHealthSlider;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        playerHealthSlider.value = health;
        if (fill != null && gradient != null)
        {
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);
            fill.color = gradient.Evaluate(playerHealthSlider.normalizedValue);
        }
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        playerHealthSlider.maxValue = health;
        healthSlider.value = health;
        playerHealthSlider.value = health;
        if (fill != null && gradient != null)
        {
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);
            fill.color = gradient.Evaluate(playerHealthSlider.normalizedValue);
        }
    }
}