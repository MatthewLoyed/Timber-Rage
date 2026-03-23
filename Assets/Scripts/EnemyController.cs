using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    private Transform playerTransform;

    [Header("Audio")]
    public AudioClip deathSound; // Drag your 'Tree Break' sound here

    void Start()
    {
        // Find the player by tag
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Move towards the player's position
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public void TakeDamage()
    {
        // 1. Route the sound through the Player's AudioSource
        // This prevents the sound from being destroyed with the enemy
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && deathSound != null)
        {
            if (player.TryGetComponent(out AudioSource playerAudio))
            {
                playerAudio.PlayOneShot(deathSound);
            }
        }

        // 2. Add Rage points
        if (RageSystem.Instance != null)
        {
            RageSystem.Instance.AddRage(15f); // Each kill = 15 rage
        }

        // 3. Remove the enemy
        Destroy(gameObject);

        if (LevelManager.Instance != null) LevelManager.Instance.RegisterKill();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(10f); // Damage amount
            }
        }
    }
}