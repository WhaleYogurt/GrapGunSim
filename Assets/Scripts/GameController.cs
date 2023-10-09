using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene management

public class GameController : MonoBehaviour
{
    public CountdownTimer countdownTimer; // Reference to the CountdownTimer script
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public Button startButton; // Reference to the start button

    private float originalSensitivity; // To store the original sensitivity

    private void Awake()
    {
        // Store the original sensitivity
        originalSensitivity = playerMovement.sensitivity;

        // Set sensitivity to 0 so the player can't look around
        playerMovement.sensitivity = 0.0f;

        // Set time to 0.0 to freeze the game initially
        // Time.timeScale = 0.0f;

        // Disable the countdown timer initially
        countdownTimer.enabled = false;
    }
    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        // Reset the time scale to normal
        Time.timeScale = 1.0f;

        // Reset the sensitivity to its original value
        playerMovement.sensitivity = originalSensitivity;

        // Enable the countdown timer to start the game
        countdownTimer.enabled = true;

        // Disable the start button so it can't be clicked again
        startButton.gameObject.SetActive(false);
    }
}
