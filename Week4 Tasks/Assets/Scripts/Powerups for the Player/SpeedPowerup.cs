using UnityEngine;

[CreateAssetMenu(fileName = "newPowerup", menuName = "Powerups/SpeedPowerup")]
public class SpeedPowerup : TimePowerup
{
    [SerializeField] private float speedIncreaseAmount = 5f;

    public override void  ApplyPowerup(GameObject target)
    {
        var movement = target.GetComponent<PlayerMovement>();

        movement.speed += speedIncreaseAmount;

    }

    public override void RemovePowerup(GameObject target)
    {
        var movement = target.GetComponent<PlayerMovement>();
        movement.speed -= speedIncreaseAmount;
    }
}
