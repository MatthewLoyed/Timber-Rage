using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Loads your main game level (Index 1 in Build Settings)
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Lumberjack has left the forest!"); // For testing in editor
        Application.Quit(); // Closes the .exe
    }
}