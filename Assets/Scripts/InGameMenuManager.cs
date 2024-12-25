using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI

    private bool isPaused = false;

    void Update()
    {
        // Toggle pause menu with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu
        Time.timeScale = 1f;          // Resume game time
        isPaused = false;             // Update state
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // Show pause menu
        Time.timeScale = 0f;          // Pause game time
        isPaused = true;              // Update state
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitting game...");
        SceneManager.LoadScene("Menu");
    }
}
