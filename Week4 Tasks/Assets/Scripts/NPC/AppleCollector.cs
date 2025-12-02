using UnityEngine;

public class AppleCollector : MonoBehaviour
{
    [SerializeField] int appleCount = 0;
    private KeyCollector collector;

    private void Awake()
    {
        collector = FindObjectOfType<KeyCollector>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            appleCount++;
            Destroy(this.gameObject);

            if (collector != null)
            {
                collector.keyCount++;
            } 
        }
    }
}