using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    private bool isPaused;

    public void TogglePause()
    {
        if(isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            isPaused = false;

        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void LoadMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
