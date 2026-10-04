using System.Collections;
using UnityEngine; //Code written by YiFan and Joelx

public class EnemyManifest : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;      
    public int maxEnemies = 10;          
    public string enemyTag = "Enemy";    

    [Header("Terrain Settings")]
    public Terrain terrain;              
    public float xMin = 0f; //boundaries of terrain
    public float xMax = 50f; //boundaries of terrain
    public float zMin = 0f;
    public float zMax = 50f;

    [Header("Spawn Timing")]
    public float spawnDelay = 1f;        

    private void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Assign enemyPrefab in the Inspector!");
            return;
        }

        if (terrain == null)
        {
            Debug.LogError("Assign your Terrain in the Inspector!");
            return;
        }

        StartCoroutine(SpawnManager()); 
    }

    private IEnumerator SpawnManager() //Assisted by AI to help with spawn code
    {
        while (true)
        {
            // Count current enemies
            int currentEnemies = GameObject.FindGameObjectsWithTag(enemyTag).Length;

            if (currentEnemies < maxEnemies)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        // Pick random X/Z within bounds
        float xPos = Random.Range(xMin, xMax);
        float zPos = Random.Range(zMin, zMax);

        // Sample the terrain height at that position
        float yPos = terrain.SampleHeight(new Vector3(xPos, 0, zPos)) + terrain.GetPosition().y; //Assisted by AI to obtain terrain height

        // Instantiate enemy
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        enemy.tag = enemyTag;

        Debug.Log($"Spawned enemy at: {enemy.transform.position}");
    }
}
