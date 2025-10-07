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
        InvokeRepeating("SpawnEnemy", 2, 5);
    }
    private void SpawnEnemy()
    {
        int r = Random.Range(0, spawnpoints.Length);

        int randomizeEnemy = Random.Range(0,2);
        if (randomizeEnemy == 0)
        {
            GameObject Tungtung = Instantiate(tungtung, spawnpoints[r].position, Quaternion.identity);
            Tungtung.transform.parent = tungtungParent.transform;
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            Tungtung.layer = enemyLayer;
        }
        else if (randomizeEnemy == 1)
        {
            GameObject Cappuccino = Instantiate(cappuccino, spawnpoints[r].position, Quaternion.identity);
            Cappuccino.transform.parent = cappuccinoParent.transform;
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            Cappuccino.layer = enemyLayer;
        }
        else if (randomizeEnemy == 2)
        {
            GameObject AirPlane = Instantiate(airplane, spawnpoints[r].position, Quaternion.identity);
            AirPlane.transform.parent = airplaneParent.transform;
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            AirPlane.layer = enemyLayer;
        }
    }
}
