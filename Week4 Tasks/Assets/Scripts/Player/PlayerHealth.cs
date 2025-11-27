using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   public int maxHealth = 100;
   public int currentHealth;
   public HealthBar healthBar;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void PlayerDamage(float damage)
    {
        currentHealth -= (int)damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
            if (UIManager.instance != null)
            {
                UIManager.instance.GameOver();
            }
        }
    }
}