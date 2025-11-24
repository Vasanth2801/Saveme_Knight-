using UnityEngine;

public class HighAreaEntrance : MonoBehaviour
{
    public Collider2D[] highAreaEntranceColliders;
    public Collider2D[] boundaryColliders;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (Collider2D col in highAreaEntranceColliders)
            {
                if (col != null)
                {
                    col.enabled = false;
                }
            }
            foreach(Collider2D col in boundaryColliders)
            {
                if (col != null)
                {
                    col.enabled = true;
                }
            } 
            other.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 13;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Collider2D col in highAreaEntranceColliders)
            {
                if (col != null)
                {
                    col.enabled = true;
                }
            }
            foreach (Collider2D col in boundaryColliders)
            {
                if (col != null)
                {
                    col.enabled = false;
                }
            }
            other.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
        }

        
    }
}
