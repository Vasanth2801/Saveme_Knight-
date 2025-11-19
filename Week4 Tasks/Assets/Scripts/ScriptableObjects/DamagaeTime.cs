using System.Collections;
using UnityEngine;

public abstract class DamagaeTime : Powerups
{
    public float damageTime = 10f;

    public abstract void RemovePowerup(GameObject target);

    public IEnumerator DamageDuration(GameObject target)
    {
        yield return new WaitForSeconds(damageTime);
        RemovePowerup(target);
    }

}
