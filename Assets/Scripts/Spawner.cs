using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Drag your Enemy PREFAB here
    public float spawnRate = 3f;
    public Vector2 spawnRangeX = new Vector2(-10, 10);
    public float spawnY = -3f; // Set this to your floor height
    public int maxEnemies = 20; // Limit the number of enemies on screen

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        // Check how many enemies exist before spawning a new one
        if (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemies) 
            return; 

        Vector3 spawnPos = new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), spawnY, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}