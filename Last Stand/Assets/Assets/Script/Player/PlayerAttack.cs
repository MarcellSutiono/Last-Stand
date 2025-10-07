using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public PlayerData pd;
    public float attackTimer;
    public Transform attackPos;
    public LayerMask enemyLayer;
    public float attackRange;
    public int damage;
    
    private Animator animator;
    private Vector2 lastMoveDirection = Vector2.down;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if (horizontal != 0 || vertical != 0)
        {
            lastMoveDirection = new Vector2(horizontal, vertical).normalized;
        }

        if (attackTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PerformAttack();
                attackTimer = pd.attackCooldown;
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void PerformAttack()
    {
        if (animator != null)
        {
            animator.SetFloat("AttackX", lastMoveDirection.x);
            animator.SetFloat("AttackY", lastMoveDirection.y);
            animator.SetTrigger("attack");
        }

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            TungTungSahur tungTung = enemiesToDamage[i].GetComponent<TungTungSahur>();
            if (tungTung != null)
            {
                tungTung.TakeDamage(damage);
            }
            
            BallerinaCappuccina ballerina = enemiesToDamage[i].GetComponent<BallerinaCappuccina>();
            if (ballerina != null)
            {
                ballerina.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}