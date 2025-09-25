using UnityEngine;

public class TungTungSahur : MonoBehaviour
{
    public float speed;
    public int health = 2;

    private bool isStunned = false;
    private float stunDuration;

    private void Update()
    {
        deathChecker();

        if(isStunned)
        {
            if (isStunned)
            {
                stunDuration -= Time.deltaTime;
                if (stunDuration <= 0f)
                {
                    isStunned = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(!isStunned)
        {
            transform.position -= new Vector3(speed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            health--;
            Destroy(col.gameObject);
        }
    }

    private void deathChecker()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void stunTungTungSahur(float duration)
    {
        isStunned = true;
        stunDuration = duration;
    }
}
