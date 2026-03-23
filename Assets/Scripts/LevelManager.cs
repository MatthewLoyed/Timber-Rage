using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int enemiesToKill = 5; // Set this in the Inspector per level
    private int currentKills = 0;
    public string nextSceneName; // Type "Level2" or "WinScene" in Inspector

    public static LevelManager Instance;

    void Awake() { Instance = this; }

    public void RegisterKill()
    {
        currentKills++;
        Debug.Log($"Enemies cleared: {currentKills}/{enemiesToKill}");

        if (currentKills >= enemiesToKill)
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next Scene Name not set in LevelManager!");
        }
    }
}