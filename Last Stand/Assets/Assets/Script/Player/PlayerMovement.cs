using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData pd;

    private Rigidbody2D rb;
    private PlayerInputSystem input;
    private Vector2 moveValue;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerInputSystem();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {

    }

    void Update()
    {
        moveValue = input.Player.Move.ReadValue<Vector2>();
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
