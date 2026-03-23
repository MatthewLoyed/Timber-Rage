using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip damageSound;

    [Header("Rage Settings")]
    public Color rageColor = new Color(0.5f, 0f, 0.5f);

    private float lastDamageTime;
    public float damageCooldown = 0.2f; // REDUCED for better feedback

    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerController playerController;

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        
        // AUTO-ASSIGN if slot is empty
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        
        if (sr != null) originalColor = sr.color;

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

        // PLAY DAMAGE SOUND
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
        else
        {
            Debug.LogWarning("Missing AudioSource or DamageSound on Player!");
        }

        if (healthSlider != null) healthSlider.value = currentHealth;
        if (sr != null) StartCoroutine(FlashRed());

        if (currentHealth <= 0) Die();
    }

    IEnumerator FlashRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (playerController != null && playerController.isRaging)
        {
            sr.color = rageColor;
        }
        else
        {
            sr.color = originalColor;
        }
    }

    void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}