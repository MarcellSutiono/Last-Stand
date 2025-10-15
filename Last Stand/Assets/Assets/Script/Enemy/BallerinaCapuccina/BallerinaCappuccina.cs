using UnityEngine;

public class BallerinaCappuccina : MonoBehaviour
{
    public int health = 1;
    public bool isAttacking = false;
    public bool isStunned = false;
    private float attackTimer = 0f;
    private float stunTimer = 0f;
    private Animator anim;

    [SerializeField] private PlayerData pd;
    [SerializeField] private BallerinaCappucinaData bcd;
    [SerializeField] private ShooterData shd;
    [SerializeField] private StunnerData std;
    private void Start()
    {
        anim = GetComponent<Animator>();
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
            if (stunTimer >= bcd.stunDuration)
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
            transform.position -= new Vector3(bcd.speed * 0.01f, 0, 0);
        }
    }

    private void attackStopwatch()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= bcd.attackCooldown)
        {
            attackTimer = bcd.attackCooldown;
        }
    }


    private void deathChecker()
    {
        if (health <= 0)
        {
            pd.exp = pd.exp + 10;
            Destroy(gameObject);
        }
    }

    public void stunCappuccino(float duration)
    {
        isStunned = true;
        bcd.stunDuration = duration;
    }

    public void knockCappuccino(float power)
    {
        transform.position += new Vector3(power, 0, 0);
    }

    public void attack(GameObject weapon)
    {
        if (attackTimer >= bcd.attackCooldown)
        {
            anim.SetTrigger("Attack");
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
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
