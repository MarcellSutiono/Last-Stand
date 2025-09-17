using UnityEngine;

public class TungtungSpawner : MonoBehaviour 
{
    public Transform[] spawnpoints;
    public GameObject tungtung;

    private void Start()
    {
        InvokeRepeating("SpawnTungtung", 2, 5);
    }
    void SpawnTungtung()
    {
        int r = Random.Range(0, spawnpoints.Length);
        GameObject Tungtung = Instantiate(tungtung, spawnpoints[r].position, Quaternion.identity);
    }

}
