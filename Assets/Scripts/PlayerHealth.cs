using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider; // We will set this up next

    private float lastDamageTime;
    public float damageCooldown = 1.0f; // Can only take damage every 1 sec

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null) 
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;

        currentHealth -= amount;
        lastDamageTime = Time.time;

        if (healthSlider != null) healthSlider.value = currentHealth;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("GAME OVER");
        // For now, just reload the scene or stop time
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}