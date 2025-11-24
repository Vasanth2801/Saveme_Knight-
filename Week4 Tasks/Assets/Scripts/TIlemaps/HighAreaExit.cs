using UnityEngine;

public class HighAreaExit : MonoBehaviour
{
    public Collider2D[] highEntranceAreaColliders;
    public Collider2D[] boundaryColliders;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach(Collider2D col  in highEntranceAreaColliders)
            {
                col.enabled = true;
            }

            foreach(Collider2D col in boundaryColliders)
            {
                col.enabled = false;
            }

            other.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
