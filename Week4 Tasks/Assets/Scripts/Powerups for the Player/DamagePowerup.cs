using UnityEngine;

[CreateAssetMenu(fileName = "newPowerup", menuName = "Powerups/DamagePowerup")]
public class DamagePowerup : DamagaeTime
{
    [SerializeField] private int damageIncreaseAmount = 20;

    public override void ApplyPowerup(GameObject target)
    {
        var attack = target.GetComponent<PlayerMovement>();
        attack.attackDamage += damageIncreaseAmount;
    }

    public override void RemovePowerup(GameObject target)
    {
        var attack = target.GetComponent<PlayerMovement>();
        attack.attackDamage -= damageIncreaseAmount;
    }
}
