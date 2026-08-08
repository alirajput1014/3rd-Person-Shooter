using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject MainMenuPanel;
    public GameObject SettingPanel;
    public GameObject ControlsPanel;
    public GameObject GameOverPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
          //  DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ShowMainMenu();
    }
    public void ShowMainMenu()
    {
        MainMenuPanel.SetActive(true);
    }
    public void PlayButton()
    {
        MainMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void SettingButton()
    {
        MainMenuPanel.SetActive(false);
        SettingPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ControlsButton()
    {
        MainMenuPanel.SetActive(false);
        ControlsPanel.SetActive(true);
    }
    public void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

    }
    public void RestartButton()
    {
        Time.timeScale = 1f;
        GameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitButton()
    {
        Application.Quit();
    }


}
