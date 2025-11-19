using UnityEngine;

public class DamagepoerupEffect : MonoBehaviour
{
    [SerializeField] private DamagePowerup damagePower;

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
         
        target.StorePowerup(damagePower);

        //Destroy the Powerup Object
        Destroy(gameObject);
    }
}
