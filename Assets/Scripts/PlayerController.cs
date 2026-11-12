using System;
using UnityEngine;
using UnityEngine.InputSystem;

// using new movement system which is somewhat different than what was taught in class
public class PlayerController : MonoBehaviour
{
    // [SerializeField] just lets you edit private values in the inspector
    [SerializeField] private float speed;
    [SerializeField] private InputActionAsset PlayerControls;
    [SerializeField] private string actionMap = "Player1";
    private float playerHeight;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool jumpInput;

    // At least keep cooldown low so that jumps arent varied heights
    private float jumpCooldown = .1f;
    private float lastJump = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PlayerControls = Instantiate(PlayerControls);
        var playerMap = PlayerControls.FindActionMap(actionMap);

        moveAction = playerMap.FindAction("Move");
        jumpAction = playerMap.FindAction("Jump");
        attackAction = playerMap.FindAction("Attack");
        rb = GetComponent<Rigidbody2D>();
        playerHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // FixedUpdate is called 50 times a second or something instead of per frame like the Update function
    // FixedUpdate is usually for physics
    void FixedUpdate()
    {
        //Debug.Log(jumpInput);
        if (IsGrounded() && (Time.time - lastJump > jumpCooldown) && jumpInput)
        {
            lastJump = Time.time;
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
        moveInput = moveAction.ReadValue<Vector2>(); // read movement keys defined as a 2D vector
        jumpInput = jumpAction.IsPressed();
        //Debug.Log(moveInput);
        Vector2 velocity = new Vector2(moveInput.x * speed, rb.linearVelocityY); // input x times speed
        rb.linearVelocity = velocity;
    }

    /// <summary>
    /// Checks if object is grounded based on its height
    /// </summary>
    /// <returns> true if hit, false otherwise </returns>
    private bool IsGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1f + .1f);
        
        if (ray.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down * (playerHeight/2 + .1f), Color.red, 1f);
            return true;
        } else
        {
            Debug.DrawRay(transform.position, Vector2.down * (playerHeight/2 + .1f), Color.green, 1f);
            return false;
        }
    }
    //these actions are enabled/disabled if this component is toggled active/unactive
    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        attackAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        attackAction.Disable();
    }

}
