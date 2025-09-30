using UnityEngine;

public class TungTungSahur : MonoBehaviour
{
    private int health = 2;
    public bool isAttacking = false;
    public bool isStunned = false;
    private float attackTimer = 0f;

    [SerializeField] private TungTungTungSahurData ttsd;
    [SerializeField] private ShooterData shd;
    [SerializeField] private StunnerData std;

    private void Update()
    {
        deathChecker();

        attackStopwatch();

        if (isStunned)
        {
            if (isStunned)
            {
                ttsd.stunDuration -= Time.deltaTime;
                if (ttsd.stunDuration <= 0f)
                {
                    isStunned = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(!isStunned && !isAttacking)
        {
            transform.position -= new Vector3(ttsd.speed * 0.01f, 0, 0);
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

    private void attackStopwatch()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= ttsd.attackCooldown)
        {
            attackTimer = ttsd.attackCooldown;
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
        ttsd.stunDuration = duration;
    }

    public void knockTungTungSahur(float power)
    {
        transform.position += new Vector3(power, 0, 0);
    }

    public void attack(GameObject weapon)
    {
        if (attackTimer >= ttsd.attackCooldown)
        {
            if (weapon.CompareTag("Shooter"))
            {
                shd.health--;
                attackTimer = 0;
                Debug.Log(shd.health);
            }
            else if (weapon.CompareTag("Stunner"))
            {
                std.health--;
                attackTimer = 0;
                Debug.Log(std.health);
            }
        }
    }
}
