using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallerinaCappuccina : MonoBehaviour
{
    public float health = 2f;
    public float maxHealth = 2f;
    public float power = 10;
    public bool isAttacking = false;
    public bool isStunned = false;
    private float attackTimer = 0f;
    private float stunTimer = 0f;
    private Animator anim;

    [SerializeField] public PlayerHealth playerHealth;

    [SerializeField] private PlayerData pd;
    [SerializeField] private BallerinaCappucinaData bcd;
    [SerializeField] private ShooterData shd;
    [SerializeField] private StunnerData std;
    [SerializeField] private KnockerData kd;
    [SerializeField] public Slider healthBar;
    [SerializeField] public int level;
    [SerializeField] public TextMeshProUGUI levelText;
    public GameObject stun;
    private void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = 2 * Mathf.Pow(1.5f, level - 1);
        power = 10 * Mathf.Pow(1.5f, level - 1);
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
            if (stunTimer >= bcd.stunDuration)
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
            pd.resource += 1;
            Destroy(gameObject);
        }
    }

    public void stunCappuccino(float duration)
    {
        isStunned = true;
        if (std.canDamage)health -=5f;
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
                
                shd.health -= power;
                attackTimer = 0;
                Debug.Log(shd.health);
            }
            else if (weapon.CompareTag("Stunner"))
            {
                std.health -= power;
                attackTimer = 0;
                Debug.Log(std.health);
            }
            else if(weapon.CompareTag("Knocker"))
            {
                kd.health -= power;
                attackTimer = 0;
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
        health -= damage;
    }
}
