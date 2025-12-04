using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePanel;

    public void Start()
    {
        pausePanel.SetActive(false);
    }

    public void OnPauseButtonClicked()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnResumeButtonClicked()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void OnMuteButtonClicked()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
