using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TungTungSahur : MonoBehaviour
{
    public float health = 6f;
    public float maxHealth = 6f;
    public float power = 10;
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
    [SerializeField] private KnockerData kd;
    [SerializeField] public Slider healthBar;
    public int level;
    [SerializeField] public TextMeshProUGUI levelText;
    public GameObject stun;

    private void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = 6 * Mathf.Pow(1.5f, level - 1);
        power = 10* Mathf.Pow(1.5f, level - 1);
        health = maxHealth;

        levelText.text = level.ToString();

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

        healthBar.value = health/maxHealth;
    }

    private void stunStopwatch()
    {
        if (isStunned)
        {
            stun.gameObject.SetActive(true);
            stunTimer += Time.deltaTime;
            if (stunTimer >= ttsd.stunDuration)
            {
                stunTimer = 0f;
                stun.gameObject.SetActive(false);
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
            pd.resource += 1;
            Destroy(gameObject);
            pd.exp = pd.exp + 10;
        }
    }
    public void stunTungTungSahur(float duration)
    {
        isStunned = true;
        if (std.canDamage)health -=5f;
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
                shd.health -= power;
                Debug.Log(shd.health);
            }
            else if (weapon.CompareTag("Stunner"))
            {
                std.health -= power;
                Debug.Log(std.health);
            }
            else if (weapon.CompareTag("Knocker"))
            {
                kd.health -= power;
                Debug.Log(kd.health);
            }
            else if (weapon.CompareTag("Player"))
            {
                playerHealth.TakeDamage(power);
            }
            attackTimer = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Tungtung: " + (health));
        health -= damage;
    }
}
