using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject noLoadData;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        string filePath = Application.persistentDataPath + "/KillState";

        
        if (File.Exists(filePath))
        {
            if (DataManager.instance == null)
            {
                GameObject dm = new GameObject("DataManager");
                dm.AddComponent<DataManager>();
            }

            if (UIManager.instance != null)
            {
                UIManager.instance.LoadData();
                SceneManager.LoadScene(1);
            }
            else
            {
                DataManager.instance.LoadFromJson();
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            noLoadData.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application Quit.....");
    }
}