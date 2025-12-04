using UnityEngine;
using System.Collections;
using TMPro; // Namespace for TextMeshPro

public class Spawner : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] public TextMeshProUGUI waveText;

    [Header("Spawn Settings")]
    public Transform[] spawnpoints;
    public float spawnRate;
    private float baseSpawnRate = 5f;

    [Header("Enemy Prefabs")]
    public GameObject tungtung;
    public GameObject cappuccino;
    public GameObject airplane;

    [Header("Enemy Parents (Containers)")]
    public GameObject tungtungParent;
    public GameObject cappuccinoParent;
    public GameObject airplaneParent;

    // State Variables
    public int wave;
    public int spawnCount;
    private int spawned;
    public int enemyLevel = 1;

    private void Start()
    {
        spawned = 0;
        spawnRate = baseSpawnRate;
        wave = 1;
        enemyLevel = 1;
        spawnCount = 2;

        UpdateWaveText();
        StartCoroutine(SpawnEnemyLoop());
    }

    // 1. Spawning Logic
    IEnumerator SpawnEnemyLoop()
    {
        // We reset spawned to 0 here to ensure the loop runs correctly
        for (spawned = 0; spawned < spawnCount; spawned++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    // 2. Wave Check Logic
    private void Update()
    {
        // Condition: Have we finished spawning AND are all enemies dead?
        if (spawned >= spawnCount) 
        {
            int totalEnemiesAlive = GetTotalEnemiesAlive();

            if (totalEnemiesAlive == 0)
            {
                NextWave();
            }
        }
    }

    // Helper function to count active enemies
    int GetTotalEnemiesAlive()
    {
        int count = 0;
        if (tungtungParent != null) count += tungtungParent.transform.childCount;
        if (cappuccinoParent != null) count += cappuccinoParent.transform.childCount;
        if (airplaneParent != null) count += airplaneParent.transform.childCount;
        return count;
    }

    void NextWave()
    {
        wave++;
        spawnCount += 2;

        // Every 5 waves logic
        if (wave % 5 == 0)
        {
            spawnRate *= 0.9f; // 10% faster
            enemyLevel++;
        }

        UpdateWaveText();
        
        // Reset spawned counter is handled inside the Coroutine loop, 
        // but calling the coroutine restarts the cycle.
        StartCoroutine(SpawnEnemyLoop());
    }

    void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + wave;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnpoints.Length == 0) return; // Safety check

        int r = Random.Range(0, spawnpoints.Length);
        float chance = Random.value;
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        GameObject spawnedEnemy = null;

        // Spawn Logic
        if (chance < 0.5f)
        {
            spawnedEnemy = Instantiate(tungtung, spawnpoints[r].position, Quaternion.identity);
            spawnedEnemy.transform.parent = tungtungParent.transform;
            
            // Attempt to set level
            var script = spawnedEnemy.GetComponent<TungTungSahur>();
            if (script != null) script.level = enemyLevel;
        }
        else if (chance < 0.8f)
        {
            spawnedEnemy = Instantiate(cappuccino, spawnpoints[r].position, Quaternion.identity);
            spawnedEnemy.transform.parent = cappuccinoParent.transform;
            
            var script = spawnedEnemy.GetComponent<BallerinaCappuccina>();
            if (script != null) script.level = enemyLevel;
        }
        else
        {
            spawnedEnemy = Instantiate(airplane, spawnpoints[r].position, Quaternion.identity);
            spawnedEnemy.transform.parent = airplaneParent.transform;
            
            var script = spawnedEnemy.GetComponent<AirPlane>();
            if (script != null) script.level = enemyLevel;
        }

        // Apply Layer
        if (spawnedEnemy != null)
        {
            spawnedEnemy.layer = enemyLayer;
        }
    }
}