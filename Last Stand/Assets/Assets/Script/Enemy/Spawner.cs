using UnityEngine;

public class Spawner : MonoBehaviour 
{
    public Transform[] spawnpoints;
    public GameObject tungtung;
    public GameObject tungtungparent;

    private void Start()
    {
        InvokeRepeating("SpawnTungtung", 2, 5);
    }
    void SpawnTungtung()
    {
        int r = Random.Range(0, spawnpoints.Length);
        GameObject Tungtung = Instantiate(tungtung, spawnpoints[r].position, Quaternion.identity);
        Tungtung.transform.SetParent(tungtungparent.transform);
    }
}
