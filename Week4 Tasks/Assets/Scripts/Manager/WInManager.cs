using System.Collections;
using TMPro;
using UnityEngine;

public class WInManager : MonoBehaviour
{
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject bgPanel;
    [SerializeField] float time = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WinGame());
        }
    }

    IEnumerator WinGame()
    { 
        yield return new WaitForSeconds(time);
        winUI.SetActive(true);
        bgPanel.SetActive(true);
        AudioManager.Instance.PlayWin();
        Time.timeScale = 0f;
    }
}