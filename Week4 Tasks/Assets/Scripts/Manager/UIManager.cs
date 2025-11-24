using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
   // [SerializeField] private TextMeshProUGUI killText;
   // [SerializeField] private int killCount;
    public EnemyKillState enemyState = new EnemyKillState();
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenu;

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
     //   killCount = 0;
       // killText.text = "KillCount: " + killCount;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void AddKill()
    {
     //   killCount++;
        DataManager.instance.enemyState.killCount += 1;
        UpdateUI();
    }

    public void SetKillCount(int count)
    {
        //  killCount = count;
        if(DataManager.instance != null)
        {
            DataManager.instance.enemyState.killCount = count;
        }
    }

    void UpdateUI()
    {
        //  killText.text = "KillCount: " + killCount.ToString();
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
          //  killText.text = "KillCount: " + DataManager.instance.enemyState.killCount;
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

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
