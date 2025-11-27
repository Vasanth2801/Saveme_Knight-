using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
            Debug.Log("Boss Triggered");
        }
    }
}
