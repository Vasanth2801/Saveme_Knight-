using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public EnemyKillState enemyState = new EnemyKillState();

    private void Awake()
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

    public void SaveFromJson()
    {
        string killState = JsonUtility.ToJson(enemyState);
        string filePath = Application.persistentDataPath + "/KillState";
        Debug.Log(filePath);
        File.WriteAllText(filePath, killState);
        Debug.Log("Save File Created");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/KillState";

        if (CheckExists(filePath))
        {
            string killState = File.ReadAllText(filePath);
            enemyState = JsonUtility.FromJson<EnemyKillState>(killState);
            if(UIManager.instance != null )
            {
                UIManager.instance.SetKillCount(enemyState.killCount);
            }
            Debug.Log("Loaded Successfully");
        }
    }

    private bool CheckExists(string filepath)
    {
        return File.Exists(filepath);
    }

    public void DeleteFromJson()
    {
        string filePath = Application.persistentDataPath + "/KillState";

        if (CheckExists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Deleted the Files");
        }
    }
}

[System.Serializable]
public class EnemyKillState
{
    public int killCount = 0;
}