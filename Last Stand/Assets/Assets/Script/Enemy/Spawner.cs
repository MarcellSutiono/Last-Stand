using UnityEngine;

public class Spawner : MonoBehaviour 
{
    public Transform[] spawnpoints;

    public GameObject tungtung;
    public GameObject tungtungParent;

    public GameObject cappuccino;
    public GameObject cappuccinoParent;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2, 5);
    }
    private void SpawnEnemy()
    {
        int r = Random.Range(0, spawnpoints.Length);

        int randomizeEnemy = Random.Range(0, 2);
        if(randomizeEnemy == 0)
        {
            GameObject Tungtung = Instantiate(tungtung, spawnpoints[r].position, Quaternion.identity);
            Tungtung.transform.parent = tungtungParent.transform;
        }
        else
        {
            GameObject Cappuccino = Instantiate(cappuccino, spawnpoints[r].position, Quaternion.identity);
            Cappuccino.transform.parent = cappuccinoParent.transform;
        }
    }
}
