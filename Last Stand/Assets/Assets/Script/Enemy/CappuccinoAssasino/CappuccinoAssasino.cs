using UnityEngine;

public class CappuccinoAssasino : MonoBehaviour
{
    private int health = 1;
    public bool isAttacking = false;
    public bool isStunned = false;
    private float attackTimer = 0f;
    private float stunTimer = 0f;

    [SerializeField] private PlayerData pd;
    [SerializeField] private CappuccinoAssasinoData cad;
    [SerializeField] private ShooterData shd;
    [SerializeField] private StunnerData std;

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
            if (stunTimer >= cad.stunDuration)
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
            transform.position -= new Vector3(cad.speed * 0.01f, 0, 0);
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
        if (attackTimer >= cad.attackCooldown)
        {
            attackTimer = cad.attackCooldown;
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
        cad.stunDuration = duration;
    }

    public void knockCappuccino(float power)
    {
        transform.position += new Vector3(power, 0, 0);
    }

    public void attack(GameObject weapon)
    {
        if (attackTimer >= cad.attackCooldown)
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
