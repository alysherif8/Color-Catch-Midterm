using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject retryButton; // Retry button UI element
    public GameObject playAgainButton; // Play Again button UI element
    public TextMeshProUGUI gameOverText; // Text element to display game-over messages

    private void Start()
    {
        retryButton.SetActive(false); // Hide the retry button initially
        playAgainButton.SetActive(false); // Hide the play again button initially
        gameOverText.gameObject.SetActive(false); // Hide game-over text initially
    }

    public void ShowGameOverUI(string message, bool isSuccess)
    {
        gameOverText.text = message; // Set the game-over message
        gameOverText.gameObject.SetActive(true); // Display the game-over text

        if (isSuccess)
        {
            playAgainButton.SetActive(true); // Show Play Again if successful
            retryButton.SetActive(false); // Hide Retry button
        }
        else
        {
            retryButton.SetActive(true); // Show Retry if unsuccessful
            playAgainButton.SetActive(false); // Hide Play Again button
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
