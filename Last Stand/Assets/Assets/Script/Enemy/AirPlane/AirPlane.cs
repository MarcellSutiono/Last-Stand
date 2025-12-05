using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AirPlane : MonoBehaviour
{
    public float health = 5f;
    public float maxHealth = 5f;
    public float power = 25;
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
    [SerializeField] private KnockerData kd;
    [SerializeField] public Slider healthBar;
    public int level;
    [SerializeField] public TextMeshProUGUI levelText;
    public GameObject stun;
    private SimpleFlash flashScript;
    private void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = 5 * Mathf.Pow(1.5f, level - 1);
        power = 25 * Mathf.Pow(1.5f, level - 1);
        health = maxHealth;
        stun.gameObject.SetActive(false);
        
        levelText.text = level.ToString();
        flashScript = GetComponent<SimpleFlash>();

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
            stunTimer += Time.deltaTime;
            stun.gameObject.SetActive(true);
            if (stunTimer >= apd.stunDuration)
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
            pd.resource += 1;
            Destroy(gameObject);
        }
    }

    public void stunAirplane(float duration)
    {
        isStunned = true;
        if (std.damage != 0)
        {
            TakeDamage(std.damage);
        }
        apd.stunDuration = duration;
    }

    public void knockAirplane(float power)
    {
        transform.position += new Vector3(power, 0, 0);
    }

    public void attack(GameObject weapon)
    {
        anim.SetTrigger("Attack");
        if (attackTimer >= apd.attackCooldown)
        {
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
            else if (weapon.CompareTag("Knocker"))
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
    public void TakeDamage(float damage)
    {
        if (isStunned && std.canKnockback)
        {
            health -= damage * 1.1f;
        }
        {
            health -= damage;
        }
        flashScript.Flash();
    }
}
