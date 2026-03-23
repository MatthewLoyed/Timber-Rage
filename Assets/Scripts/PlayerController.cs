using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    [Header("Combat Settings")]
    public Transform attackPoint; 
    public float attackRange = 0.5f;

    [Header("Audio Settings")]
    public AudioClip attackSound; 
    public AudioClip rageSound; // 1. ADDED THIS SLOT
    private AudioSource audioSource;

    [Header("Rage Settings")]
    public bool isRaging = false; 

    private Rigidbody2D rb;
    private BoxCollider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        
        // AUTO-ASSIGN if null
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // 1. Horizontal Movement
        float moveInput = 0;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) 
            moveInput = -1;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) 
            moveInput = 1;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 2. Jumping
        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 3. Attacking
        if (Keyboard.current.kKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D obj in hitObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                if (obj.TryGetComponent(out EnemyController enemyScript))
                {
                    enemyScript.TakeDamage();
                }
            }
        }
    }

    // 2. ADDED THIS FUNCTION
    public void ActivateRage()
    {
        if (isRaging) return; // Safety check: don't play if already raging

        isRaging = true;
        
        if (audioSource != null && rageSound != null)
        {
            audioSource.PlayOneShot(rageSound);
        }
    }

    // Call this when rage ends
    public void DeactivateRage()
    {
        isRaging = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}