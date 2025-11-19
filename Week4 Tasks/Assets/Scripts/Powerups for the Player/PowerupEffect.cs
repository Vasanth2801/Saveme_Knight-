using UnityEngine;

public class PowerupEffect : MonoBehaviour
{
    [SerializeField] private SpeedPowerup powerup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerMovement = other.GetComponent<PlayerMovement>();
            ActivatePowerup(playerMovement);
        }
    }

    void ActivatePowerup(PlayerMovement target)
    {
        //Check the Collison 
        Debug.Log("Powerup Collected");

        target.ApplyPowerup(powerup);

        //Destroy the Powerup Object
        Destroy(gameObject);
    }
}
