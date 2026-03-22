using UnityEngine;
using UnityEngine.UI; // Required for the Slider

public class RageSystem : MonoBehaviour
{
    public static RageSystem Instance; // Singleton pattern for easy access

    [Header("Rage Settings")]
    public float maxRage = 100f;
    public float currentRage = 0f;
    public Slider rageSlider; // Drag your UI Slider here

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Initialize UI
        if (rageSlider != null)
        {
            rageSlider.maxValue = maxRage;
            rageSlider.value = currentRage;
        }
    }

    public void AddRage(float amount)
    {
        currentRage += amount;
        currentRage = Mathf.Clamp(currentRage, 0, maxRage);
        
        // Update the UI
        if (rageSlider != null)
        {
            rageSlider.value = currentRage;
        }

        Debug.Log("Current Rage: " + currentRage);

        if (currentRage >= maxRage)
        {
            EnterRageMode();
        }
    }

    void EnterRageMode()
    {
        Debug.Log("!!! RAGE MODE ACTIVATED !!!");
        // Later: Tell PlayerController to move faster
    }
}