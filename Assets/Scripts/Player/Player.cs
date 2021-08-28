using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variabl//
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundDist = 0.4f;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] public float health_P = 100f;

    public Transform playerBody;
    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;
    Vector3 move;
    bool isGrounded;

    //References//
    //[SerializeField] private Ui_Inventory ui_Inventory;
    CharacterController characterController;
    Animator playerAnimator;
    Shoot shoot;
    //private Inventory inventory;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        shoot = FindObjectOfType<Shoot>();
    }

    private void Awake()
    {
        //inventory = new Inventory();
        //ui_Inventory.SetInventory(inventory);
    }

    void Update()
    {   
        RotateMove();
        Run();
        Attack();
    }


    private void RotateMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);
    }
    private void Attack()
    {
        //bool isHoldingBow;
        if(Input.GetMouseButtonDown(1))
        {
            shoot.StartShooting();
        }
    }
    private void Run()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        characterController.Move(Vector3.ClampMagnitude(move, 1.0f) * runSpeed * Time.deltaTime);
        LetsRun();
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void ReduceHealth(float amount)
    {
        health_P -= amount;
        if (health_P <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void LetsRun()
    {
        bool isRunningX = Mathf.Abs(move.x) > Mathf.Epsilon;
        bool isRunningZ = Mathf.Abs(move.z) > Mathf.Epsilon;

        if (isRunningX || isRunningZ)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }
}
