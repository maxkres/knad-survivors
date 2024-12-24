using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Tooltip("The name of the scene to load for the game.")]
    [SerializeField] private string playSceneName;

    /// <summary>
    /// Loads the specified play scene.
    /// </summary>
    public void PlayGame()
    {
        if (!string.IsNullOrEmpty(playSceneName))
        {
            SceneManager.LoadScene(playSceneName);
        }
        else
        {
            Debug.LogWarning("Play scene name is not set in the MenuManager!");
        }
    }

    /// <summary>
    /// Exits the application.
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit();
    }
}

