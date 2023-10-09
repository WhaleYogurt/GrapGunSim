using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 10.0f;
    private float currentTime;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI scoreText;

    public GameObject endGamePanel; // UI panel to be shown at the end

    public float slowDownDuration = 2.0f;

    public ScoreCounter userScore;

    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public GameObject objectToDisable; // Reference to the GameObject you want to disable
    public GameObject objectToEnable;  // Reference to the GameObject you want to enable

    private void Start()
    {
        currentTime = countdownTime;
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (currentTime > 0)
        {
            countdownText.text = currentTime.ToString();
            yield return new WaitForSeconds(1.0f);
            currentTime -= 1;
        }

        StartCoroutine(SlowDownTime());
    }

    private IEnumerator SlowDownTime()
    {
        float originalTimeScale = Time.timeScale;
        float originalSensitivity = playerMovement.sensitivity;
        float elapsedTime = 0f;

        while (Time.timeScale > 0)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(originalTimeScale, 0, elapsedTime / slowDownDuration);

            // Adjusting the sensitivity of the PlayerMovement script
            playerMovement.sensitivity = Mathf.Lerp(originalSensitivity, 0, elapsedTime / slowDownDuration);

            yield return null;
        }

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EndGame();
    }

    private void EndGame()
    {
        countdownText.enabled = false;
        scoreText.text = "" + userScore.Score;
        objectToDisable.SetActive(false);
        objectToEnable.SetActive(true);
        endGamePanel.SetActive(true); // Activate the end game panel
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
