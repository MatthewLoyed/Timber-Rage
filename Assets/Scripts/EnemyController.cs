using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Movement")]
    public float speed = 2f;
    private Transform playerTransform;

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
        // Tell the RageSystem to add points
        if (RageSystem.Instance != null)
        {
            RageSystem.Instance.AddRage(15f); // Each kill = 15 rage
        }

        Destroy(gameObject);
    }

    // This runs automatically when two 2D colliders touch
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