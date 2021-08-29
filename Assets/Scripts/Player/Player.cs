using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variabl//
    public float health_P = 100;

    [SerializeField]
    float playerSpeed = 2.0f;

    [SerializeField]
    float gravityValue = -9.81f;

    [SerializeField]
    float turnSpeed = 5f;

    bool groundedPlayer;

    Vector3 playerVelocity;

    CharacterController controller;

    Animator animator;

    Transform cameraTransform;

    //References//
    Shoot shoot;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        shoot = FindObjectOfType<Shoot>();
    }

    void Update()
    {   
        ////Movement/////
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0;

        controller.Move(playerSpeed * Time.deltaTime * move);
        animator.SetFloat("move", move.magnitude);

        if (move.magnitude > 0)
        {
            Quaternion newDirection = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        //////////////////

        Attack();
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
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        // if (isGrounded && velocity.y < 0)
        // {
        //     velocity.y = -2f;
        // }

        // float x = Input.GetAxis("Horizontal");
        // float z = Input.GetAxis("Vertical");

        // move = transform.right * x + transform.forward * z;

        // characterController.Move(Vector3.ClampMagnitude(move, 1.0f) * runSpeed * Time.deltaTime);
        // LetsRun();
        // velocity.y += gravity * Time.deltaTime;
        // characterController.Move(velocity * Time.deltaTime);
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
        // bool isRunningX = Mathf.Abs(move.x) > Mathf.Epsilon;
        // bool isRunningZ = Mathf.Abs(move.z) > Mathf.Epsilon;

        // if (isRunningX || isRunningZ)
        // {
        //     playerAnimator.SetBool("isRunning", true);
        // }
        // else
        // {
        //     playerAnimator.SetBool("isRunning", false);
        // }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.transform.GetComponent<DamageDealer>())
            ReduceHealth(hit.collider.transform.GetComponent<DamageDealer>().GetDamage());
    }
}
