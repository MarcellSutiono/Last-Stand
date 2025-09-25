using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData pd;

    private Rigidbody2D rb;
    private PlayerInputSystem input;
    private Vector2 moveValue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerInputSystem();
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
        move();
    }

    private void move()
    { 
        rb.linearVelocity = moveValue * pd.playerSpeed;
    }
}
