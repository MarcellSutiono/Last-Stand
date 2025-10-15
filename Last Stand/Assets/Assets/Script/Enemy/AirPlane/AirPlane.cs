using UnityEngine;

public class AirPlane : MonoBehaviour
{
    public int health = 1;
    public bool isAttacking = false;
    public bool isStunned = false;
    private float attackTimer = 0f;
    private float stunTimer = 0f;
    private Animator anim;

    [SerializeField] public PlayerHealth playerHealth;

    [SerializeField] private PlayerData pd;
    [SerializeField] private AirPlaneData apd;
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
            if (stunTimer >= apd.stunDuration)
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
            transform.position -= new Vector3(apd.speed * 0.01f, 0, 0);
        }
    }

    private void attackStopwatch()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= apd.attackCooldown)
        {
            attackTimer = apd.attackCooldown;
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

    public void stunAirplane(float duration)
    {
        isStunned = true;
        apd.stunDuration = duration;
    }

    public void knockAirplane(float power)
    {
        transform.position += new Vector3(power, 0, 0);
    }

    public void attack(GameObject weapon)
    {
        if (attackTimer >= apd.attackCooldown)
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
