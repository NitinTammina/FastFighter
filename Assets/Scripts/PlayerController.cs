using System;
using UnityEngine;
using UnityEngine.InputSystem;

// using new movement system which is somewhat different than what was taught in class
public class PlayerController : MonoBehaviour
{
    // [SerializeField] just lets you edit private values in the inspector
    [SerializeField] private float speed;
    [SerializeField] private InputActionAsset PlayerControls;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        moveAction = PlayerControls.FindAction("Move");
        jumpAction = PlayerControls.FindAction("Jump");
        //attack action is just a filler for testing the 
        attackAction = PlayerControls.FindAction("Attack");
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called 50 times a second or something instead of per frame like the Update function
    // FixedUpdate is usually for physics
    void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>(); // read movement keys defined as a 2D vector
        //Debug.Log(moveInput);
        Vector2 velocity = new Vector2(moveInput.x * speed, rb.linearVelocityY); // input x times speed
        rb.linearVelocity = velocity;
    }

    // these actions are enabled/disabled if this component is toggled active/unactive
    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

}
