using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;
    [Header("No Load Data Panel")]
    [SerializeField] private GameObject noLoadData;

    [Header("Graphics Settings")]
    private int _qualityIndex;
    private bool _isFullScreen;

    [Header("Resolution Settings")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

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

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
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
                SceneManager.LoadScene(2);
            }
            else
            {
                DataManager.instance.LoadFromJson();
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            noLoadData.SetActive(true);
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
         _qualityIndex = qualityIndex;
    }

    public void ApplyGraphics()
    {
        PlayerPrefs.SetInt("QualityIndex", _qualityIndex);
        QualitySettings.SetQualityLevel(_qualityIndex);

        PlayerPrefs.SetInt("IsFullScreen", _isFullScreen ? 1 : 0);
        Screen.fullScreen = _isFullScreen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}