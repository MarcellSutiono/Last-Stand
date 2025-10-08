using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    private Rigidbody2D rb;
    private Vector2 moveValue;
    private Animator anim;
    public ShooterData shd;
    public StunnerData std;

    private void Awake()
    {
        //player data
        pd.level = 1;
        pd.exp = 0;
        pd.expNeeded = 100;
        pd.resource = 0;

        //shooter data
        shd.level = 1;

        //stunner data
        std.level = 1;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(moveValue != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("InputX", moveValue.x);
            anim.SetFloat("InputY", moveValue.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetFloat("InputX", 0);
            anim.SetFloat("InputY", 0);
        }
        move();
    }

    private void move()
    { 
        rb.linearVelocity = moveValue * pd.playerSpeed;
    }
}
