using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 8f;
    private int health = 2;
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
            if (sd.level == 1)
            {
                col.gameObject.GetComponent<TungTungSahur>().health--;
                health -= 2;
            }
            else if (sd.level == 2)
            {
                col.gameObject.GetComponent<TungTungSahur>().health -= 2;
                health -= 2;
            }
            else if (sd.level >= 3)
            {
                col.gameObject.GetComponent<TungTungSahur>().health -= 2;
                health -= 1;
            }
        }
        else if(col.gameObject.CompareTag("Ballerina"))
        {
            if (sd.level == 1)
            {
                col.gameObject.GetComponent<BallerinaCappuccina>().health--;
                health -= 2;
            }
            else if (sd.level == 2)
            {
                col.gameObject.GetComponent<BallerinaCappuccina>().health -= 2;
                health -= 2;
            }
            else if (sd.level >= 3)
            {
                col.gameObject.GetComponent<BallerinaCappuccina>().health -= 2;
                health -= 1;
            }
        }
        else if (col.gameObject.CompareTag("Airplane"))
        {
            if (sd.level == 1)
            {
                col.gameObject.GetComponent<AirPlane>().health--;
                health -= 2;
            }
            else if (sd.level == 2)
            {
                col.gameObject.GetComponent<AirPlane>().health -= 2;
                health -= 2;
            }
            else if (sd.level >= 3)
            {
                col.gameObject.GetComponent<AirPlane>().health -= 2;
                health -= 1;
            }
        }
    }
}
