using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 8f;
    private int health = 1;
    public ShooterData sd;
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TungTung"))
        {
            col.gameObject.GetComponent<TungTungSahur>().TakeDamage(sd.damage);
            health --;
        }
        else if(col.gameObject.CompareTag("Ballerina"))
        {
            col.gameObject.GetComponent<BallerinaCappuccina>().TakeDamage(sd.damage);
            health --;
        }
        else if (col.gameObject.CompareTag("Airplane"))
        {
            col.gameObject.GetComponent<AirPlane>().TakeDamage(sd.damage);
            health --;
        }
    }
}
