using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private int currency;
    public EnemyKillState enemyState = new EnemyKillState();
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenu;
    public GameObject bgPanel;

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
       currency = 0;
       currencyText.text = "Currency: " + currency;
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
        bgPanel.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        bgPanel.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void AddKill()
    {
        currency += 10;
        DataManager.instance.enemyState.killCount += 10;
        UpdateUI();
    }

    public void SetKillCount(int count)
    {
        currency = count;
        if(DataManager.instance != null)
        {
            DataManager.instance.enemyState.killCount = count;
        }
    }

    void UpdateUI()
    {
        currencyText.text = "Currency: " + currency.ToString();
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
           currencyText.text = "Currency: " + DataManager.instance.enemyState.killCount;
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
        SceneManager.LoadScene(2);
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