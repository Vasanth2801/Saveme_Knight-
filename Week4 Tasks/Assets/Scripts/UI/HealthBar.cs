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
        if (healthSlider != null)
            healthSlider.value = health;
        if (playerHealthSlider != null)
            playerHealthSlider.value = health;
        if (fill != null && gradient != null)
        {
            if (healthSlider != null)
                fill.color = gradient.Evaluate(healthSlider.normalizedValue);
            else if (playerHealthSlider != null)
                fill.color = gradient.Evaluate(playerHealthSlider.normalizedValue);
        }
    }

    public void SetMaxHealth(int health)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }
        if (playerHealthSlider != null)
        {
            playerHealthSlider.maxValue = health;
            playerHealthSlider.value = health;
        }
        if (fill != null && gradient != null)
        {
            if (healthSlider != null)
            {
                fill.color = gradient.Evaluate(healthSlider.normalizedValue);
            }
            else if (playerHealthSlider != null)
            {
                fill.color = gradient.Evaluate(playerHealthSlider.normalizedValue);
            }
        }
    }
}