using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
