using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [SerializeField] float duration;

    public void Chestbehavior()
    {
        Destroy(gameObject);
    }
}
