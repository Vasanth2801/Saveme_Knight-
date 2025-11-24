using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   public static PlayerHealth instance;
   
   public int maxHealth = 100;
   public int currentHealth;
   public HealthBar healthBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void PlayerDamage(float damage)
    {
        currentHealth -= (int)damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
            UIManager.instance.GameOver();
        }
    }
}
