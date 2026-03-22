using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void TakeDamage()
    {
        // For now, enemies just vanish. 
        // Later, this is where we'll tell the RageSystem: "Add +10 Rage!"
        Debug.Log("Enemy Died!");
        Destroy(gameObject); 
    }
}