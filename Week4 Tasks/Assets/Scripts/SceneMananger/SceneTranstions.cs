using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTranstions : MonoBehaviour
{
    [SerializeField] bool isOpen;
    KeyCollector collector;
    [SerializeField] float duration;
    [SerializeField] float showDuration;
    [SerializeField] GameObject needsKey;

    void Start()
    {
        collector = FindObjectOfType<KeyCollector>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SceneLoad());
        }
    }

    IEnumerator SceneLoad()
    {
        if(!isOpen)
        {
            if(collector != null)
            {
                if(collector.keyCount >= 2)
                {
                    SceneManager.LoadScene(3);
                    yield return new WaitForSeconds(duration);
                }
                else
                {
                    StartCoroutine(ShowKey());
                }
            }
        }
    }

    IEnumerator ShowKey()
    {
        if (!isOpen)
        {
            needsKey.SetActive(true);
            yield return new WaitForSeconds(showDuration);
            needsKey.SetActive(false);
        }
    }
}