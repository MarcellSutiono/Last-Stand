using UnityEngine;

public class TungTungSahur : MonoBehaviour
{
    public int health = 2;
    public bool isAttacking = false;
    public bool isStunned = false;
    private float attackTimer = 0f;
    private float stunTimer = 0f;
    private Animator anim;
    [SerializeField] public PlayerHealth playerHealth;

    [SerializeField] private PlayerData pd;
    [SerializeField] private TungTungTungSahurData ttsd;
    [SerializeField] private ShooterData shd;
    [SerializeField] private StunnerData std;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        deathChecker();
        attackStopwatch();
        stunStopwatch();
    }

    private void stunStopwatch()
    {
        if (isStunned)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= ttsd.stunDuration)
            {
                stunTimer = 0f;
                isStunned = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isStunned && !isAttacking)
        {
            transform.position -= new Vector3(ttsd.speed * 0.01f, 0, 0);
        }
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.CompareTag("Bullet"))
    //    {
    //        if(shd.level == 1)
    //        {
    //            health--;
    //            Destroy(col.gameObject);
    //        }
    //        else if(shd.level == 2)
    //        {
    //            health -=2;
    //            Destroy(col.gameObject);
    //        }
    //        else if(shd.level == 3)
    //        {
    //            health -= 2;
    //            Destroy(col.gameObject);
    //        }
    //    }
    //}

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
            pd.exp = pd.exp + 10;
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
            anim.SetTrigger("Attack");

            if (weapon.CompareTag("Shooter"))
            {
                shd.health--;
                Debug.Log(shd.health);
            }
            else if (weapon.CompareTag("Stunner"))
            {
                std.health--;
                Debug.Log(std.health);
            }
            else if (weapon.CompareTag("Player"))
            {
                playerHealth.TakeDamage(pd.damageTaken);
            }
            attackTimer = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
