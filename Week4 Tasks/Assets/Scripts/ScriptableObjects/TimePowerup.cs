using System.Collections;
using UnityEngine;

public abstract class TimePowerup : Powerups
{
    public float duration = 5f;

    public abstract void RemovePowerup(GameObject target);

    public IEnumerator PowerupDuration(GameObject target)
    {
        yield return new WaitForSeconds(duration);
        RemovePowerup(target);
    }
}