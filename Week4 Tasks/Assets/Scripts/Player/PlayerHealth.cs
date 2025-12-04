using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider playerSlider;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (playerSlider != null)
        {
            playerSlider.maxValue = maxHealth;
            playerSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning($"PlayerHealth: 'playerSlider' not assigned on {gameObject.name}");
        }
    }

    public void PlayerDamage(float damage)
    {
        currentHealth -= (int)damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (playerSlider != null)
        {
            playerSlider.value = currentHealth;
        }

        Debug.Log($"PlayerDamage {gameObject.name} dmg={damage} cur={currentHealth}");

        if (currentHealth <= 0)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayDeath();
            }
            else
            {
                Debug.LogWarning("PlayerHealth: AudioManager.Instance is null");
            }

            currentHealth = 0;

            if (UIManager.instance != null)
            {
                UIManager.instance.GameOver();
            }
            else
            {
                Debug.LogWarning("PlayerHealth: UIManager.instance is null - GameOver won't be shown");
            }

            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false; 
        }
    }
}