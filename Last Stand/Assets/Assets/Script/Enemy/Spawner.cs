using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject tungtung;
    public GameObject tungtungParent;

    public GameObject cappuccino;
    public GameObject cappuccinoParent;

    public GameObject airplane;
    public GameObject airplaneParent;

    private void Start()
    {
        StartCoroutine(SpawnEnemyLoop());
    }

    private System.Collections.IEnumerator SpawnEnemyLoop()
    {
        while (true)
        {
            SpawnEnemy();
            // Wait between 1 and 2 seconds before spawning again
            float delay = Random.Range(1f, 2f);
            yield return new WaitForSeconds(delay+4f);
        }
    }

    private void SpawnEnemy()
    {
        int r = Random.Range(0, spawnpoints.Length);
        float chance = Random.value; // random float between 0.0 and 1.0
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (chance < 0.5f) // 50% chance for Tungtung
        {
            GameObject Tungtung = Instantiate(tungtung, spawnpoints[r].position, Quaternion.identity);
            Tungtung.transform.parent = tungtungParent.transform;
            Tungtung.layer = enemyLayer;
        }
        else if (chance < 0.8f) // Next 30% chance for Cappuccino (0.5 - 0.8)
        {
            GameObject Cappuccino = Instantiate(cappuccino, spawnpoints[r].position, Quaternion.identity);
            Cappuccino.transform.parent = cappuccinoParent.transform;
            Cappuccino.layer = enemyLayer;
        }
        else // Remaining 20% chance for Airplane/Bombardino (0.8 - 1.0)
        {
            GameObject AirPlane = Instantiate(airplane, spawnpoints[r].position, Quaternion.identity);
            AirPlane.transform.parent = airplaneParent.transform;
            AirPlane.layer = enemyLayer;
        }
    }
}
