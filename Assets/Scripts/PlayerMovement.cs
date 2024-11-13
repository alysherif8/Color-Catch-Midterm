using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f; // Speed at which the player moves
    private GameTimer gameTimer; // Reference to the GameTimer to check if the game has ended
    private Transform cameraTransform; // Reference to the camera's transform for relative movement

    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>(); // Find and assign the GameTimer in the scene

        // Find the Main Camera's Transform
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        // Only allow movement if the game hasn't ended
        if (gameTimer != null && !gameTimer.gameEnded)
        {
            // Get input for horizontal and vertical movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Convert input to movement direction relative to the camera
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Ignore vertical direction of camera to keep movement flat
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // Calculate the movement direction
            Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized;

            // Apply movement
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with a collectible and the game hasn't ended
        if (other.CompareTag("Collectible") && gameTimer != null && !gameTimer.gameEnded)
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>(); // Find the ScoreManager
            string targetColor = scoreManager.GetTargetColor(); // Get the target color

            // Get the collectible's color from its material name
            string collectedColor = other.gameObject.GetComponent<Renderer>().material.name;

            // Check if the collected color matches the target color
            if (collectedColor.Contains(targetColor))
            {
                scoreManager.AddScore(1, targetColor); // Correct color collected, add score
                Destroy(other.gameObject); // Remove the collectible
            }
            else
            {
                scoreManager.AddScore(-1, collectedColor); // Wrong color collected, deduct score
            }
        }
    }
}
