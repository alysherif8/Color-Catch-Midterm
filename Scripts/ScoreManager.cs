using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Tracks the player's score
    public TextMeshProUGUI scoreText; // UI Text for displaying score
    public TextMeshProUGUI targetColorText; // UI Text for displaying the target color
    public TextMeshProUGUI wrongColorText; // UI Text for displaying "Wrong Color!" message

    private string targetColor; // Stores the current target color
    private Dictionary<string, int> colorAssignments = new Dictionary<string, int>
    {
        { "Red", 4 }, // Initial count of Red collectibles
        { "Blue", 4 }, // Initial count of Blue collectibles
        { "Green", 4 } // Initial count of Green collectibles
    };

    private void Start()
    {
        AssignNewTargetColor(); // Set the initial target color
        UpdateScoreText(); // Initialize the score display
        wrongColorText.gameObject.SetActive(false); // Hide the wrong color message initially
    }

    private void AssignNewTargetColor()
    {
        List<string> availableColors = new List<string>();

        // Add colors with remaining collectibles to availableColors
        foreach (var color in colorAssignments)
        {
            if (color.Value > 0)
            {
                availableColors.Add(color.Key);
            }
        }

        // Select a random color from available colors
        if (availableColors.Count > 0)
        {
            targetColor = availableColors[Random.Range(0, availableColors.Count)];
            targetColorText.text = "Collect: " + targetColor;
        }
        else
        {
            targetColorText.text = "All collectibles collected!";
        }
    }

    public void AddScore(int points, string collectedColor)
    {
        // Adjust score and update display
        if (collectedColor == targetColor)
        {
            score += points; // Add points for correct color
            UpdateScoreText();
            if (colorAssignments.ContainsKey(targetColor))
            {
                colorAssignments[targetColor]--; // Reduce count of collected color
            }
            AssignNewTargetColor(); // Set a new target color
        }
        else
        {
            score -= 1; // Deduct a point for incorrect color
            UpdateScoreText();
            StartCoroutine(ShowWrongColorMessage()); // Display "Wrong Color!" message
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private IEnumerator ShowWrongColorMessage()
    {
        wrongColorText.gameObject.SetActive(true); // Show the wrong color message
        yield return new WaitForSeconds(1f); // Display for 1 second
        wrongColorText.gameObject.SetActive(false); // Hide the message
    }

    public string GetTargetColor()
    {
        return targetColor; // Return the current target color
    }
}
