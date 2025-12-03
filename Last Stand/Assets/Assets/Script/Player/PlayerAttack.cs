using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public PlayerData pd;
    public float attackTimer;
    public Transform attackPosUp;
    public Transform attackPosDown;
    public Transform attackPosLeft;
    public Transform attackPosRight;
    public LayerMask enemyLayer;
    public float attackRange = 5f;
    public int damage = 1;
    public Transform currentAttackPos;
    
    private Animator animator;
    private Vector2 lastMoveDirection;
    private Vector2 dir;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        lastMoveDirection = GetComponent<PlayerMovement>().lastMoveDirection;

        dir = lastMoveDirection;
        

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
            animator.SetFloat("AttackX", dir.x);
            animator.SetFloat("AttackY", dir.y);
            animator.SetTrigger("attack");
        }

        currentAttackPos = GetAttackPosition();
        Debug.Log(currentAttackPos);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            TungTungSahur tungTung = enemiesToDamage[i].GetComponent<TungTungSahur>();
            if (tungTung != null)
            {
                Debug.Log("Hit");
                tungTung.TakeDamage(damage);
            }

            BallerinaCappuccina ballerina = enemiesToDamage[i].GetComponent<BallerinaCappuccina>();
            if (ballerina != null)
            {
                Debug.Log("Hit");
                ballerina.TakeDamage(damage);
            }
            
            AirPlane airplane = enemiesToDamage[i].GetComponent<AirPlane>();
            if (airplane != null && airplane.isStunned)
            {
                Debug.Log("Hit");
                airplane.TakeDamage(damage);
            }
        }
    }

    Transform GetAttackPosition()
    {
        if (Mathf.Abs(lastMoveDirection.x) > Mathf.Abs(lastMoveDirection.y))
        {
            if (lastMoveDirection.x > 0)
            {
                Debug.Log("Attack R");
                return attackPosRight;
            }
            else
            {
                Debug.Log("Attack L");
                return attackPosLeft;
            }
        }
        else
        {
            if (lastMoveDirection.y > 0)
            {
                Debug.Log("Attack U");
                return attackPosUp;
            }
            else
            {
                Debug.Log("Attack D");
                return attackPosDown;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(currentAttackPos != null)
            Gizmos.DrawWireSphere(currentAttackPos.position, attackRange);
    }
}