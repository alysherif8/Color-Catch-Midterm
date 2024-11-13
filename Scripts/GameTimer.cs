using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 45f; // Time in seconds for the countdown timer
    public TextMeshProUGUI timerText; // UI Text component to display the remaining time
    public bool gameEnded = false; // Flag to check if the game has ended
    private UIManager uiManager; // Reference to the UIManager for handling game-over UI

    private void Start()
    {
        UpdateTimerText(); // Initialize the timer display
        uiManager = FindObjectOfType<UIManager>(); // Find and assign the UIManager in the scene
    }

    private void Update()
    {
        // Only update the timer if the game hasn't ended
        if (!gameEnded)
        {
            // Check if there is time left
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Decrease the timer by the time since the last frame
                UpdateTimerText(); // Update the timer text display
            }

            // Check for end conditions: no collectibles or time is up
            if (GameObject.FindGameObjectsWithTag("Collectible").Length == 0 && !gameEnded)
            {
                EndGame(true); // End the game with success if all collectibles are collected
            }
            else if (timeRemaining <= 0)
            {
                EndGame(false); // End the game without success if the timer runs out
            }
        }
    }

    // Updates the timer display with the remaining time in seconds
    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining) + " Seconds";
    }

    // Ends the game and shows the game-over message
    private void EndGame(bool allCollected)
    {
        gameEnded = true; // Set the game-ended flag to stop the timer
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>(); // Find the ScoreManager in the scene
        int finalScore = scoreManager.score; // Get the player's final score

        // Show a message based on whether all collectibles were collected
        if (allCollected)
        {
            uiManager.ShowGameOverUI("Congrats! You collected all collectibles!\nYour Final Score is: " + finalScore, true);
        }
        else
        {
            uiManager.ShowGameOverUI("Game Over!\nYour Final Score is: " + finalScore, false);
        }
        
        timerText.gameObject.SetActive(false); // Hide the timer display after the game ends
    }
}
