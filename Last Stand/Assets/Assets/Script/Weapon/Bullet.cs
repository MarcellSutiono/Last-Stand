using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 8f;
    public ShooterData sd;
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
