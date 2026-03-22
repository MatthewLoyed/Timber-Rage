using UnityEngine;
using UnityEngine.SceneManagement; // Required to restart the level

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing that fell is the Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Lumberjack overboard! Resetting...");
            RestartLevel();
        }
        else if (other.CompareTag("Enemy"))
        {
            // Optional: Destroy enemies that fall off too
            Destroy(other.gameObject);
        }
    }

    void RestartLevel()
    {
        // Get the current active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}