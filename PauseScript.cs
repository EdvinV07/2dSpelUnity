using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1; // Freeze time when paused
    }

    public void Resume()
    {
        TogglePause();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        // Add code to handle options menu here
        Debug.Log("Options menu not implemented yet!");
    }
}