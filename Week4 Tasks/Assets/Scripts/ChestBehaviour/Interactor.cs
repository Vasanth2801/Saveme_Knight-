using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{

    public bool isRange;
    public UnityEvent interaction;

    private void Update()
    {
        if(isRange && Input.GetKeyDown(KeyCode.E))
        {
            interaction.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isRange = false;
        }
    }
}