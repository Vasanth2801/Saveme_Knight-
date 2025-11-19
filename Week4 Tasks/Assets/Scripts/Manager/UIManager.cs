using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private int killCount;
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

    private void Start()
    {
        killCount = 0;
        killText.text = "KillCount: " + killCount;
    }

    public void AddKill()
    {
        killCount++;
        DataManager.instance.enemyState.killCount += 1;
        UpdateUI();
    }

    public void SetKillCount(int count)
    {
        killCount = count;
        if(DataManager.instance != null)
        {
            DataManager.instance.enemyState.killCount = count;
        }
    }

    void UpdateUI()
    {
        killText.text = "KillCount: " + killCount.ToString();
    }

    public void SaveData()
    {
        if(DataManager.instance != null)
        {
            DataManager.instance.SaveFromJson();
        }
        else
        {
            Debug.Log("No Save File Found");
        }
    
    }

    public void LoadData()
    {
        if(DataManager.instance != null)
        {
            DataManager.instance.LoadFromJson();
            killText.text = "KillCount: " + DataManager.instance.enemyState.killCount;
            SetKillCount(DataManager.instance.enemyState.killCount);
        }
        else
        {
            Debug.Log("No Data Found");
        }
        
    }

    public void DeleteData()
    {
        DataManager.instance.DeleteFromJson();
    }
}
