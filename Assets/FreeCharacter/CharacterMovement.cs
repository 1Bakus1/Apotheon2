using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private MovementMotor motor;
    public float move_Magnitude = 0.05f;
    public float speed;
    public float turnSpeed = 10f;
    public float speed_Jump = 20f;
    private float speed_Move_Multiplier = 10f;
    private Vector3 direction;
    private Animator anim;
    private Camera mainCamera;
    public int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        motor = GetComponent<MovementMotor>();
        anim = GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
    }

    void Start()
    {
        anim.applyRootMotion = false;
        mainCamera = Camera.main;
    }

    void Update()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation,
                                                      Time.deltaTime * turnSpeed);
            }
        MovementAndJumping();
    }

    private Vector3 MoveDirection
    {
        get { return direction; }
        set
        { 
            
            direction = value * speed_Move_Multiplier;
            
            //direction *= speed * (Vector3.Dot(transform.forward, direction) + 1f) * 5f;
            motor.Move(direction);
            AnimationMove(motor.charController.velocity.magnitude * 0.1f);
            

            //Sterowanie bez myszki samymi klawiszami
            /*
            direction = value * speed_Move_Multiplier;
            if (direction.magnitude > 0.1f)
            {
                var newRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation,
                                                      Time.deltaTime * turnSpeed);
            }
            direction *= speed * (Vector3.Dot(transform.forward, direction) + 1f) * 5f;
            motor.Move(direction);
            AnimationMove(motor.charController.velocity.magnitude * 0.1f);
            */
        }
    }

    void Moving(Vector3 dir, float mult)
    {
        speed_Move_Multiplier = 1 * mult;
        MoveDirection = dir;
    }

    [HideInInspector]
    public void Jump()
    {
        motor.Jump(speed_Jump);
    }

    void AnimationMove(float magnitude)
    {
        if (magnitude > move_Magnitude)
        {
            float speed_Animation = magnitude * 2f;

            if (speed_Animation < 1f)
                speed_Animation = 1f;

          
            anim.SetBool("IsWalking", true);
            anim.speed = speed_Animation;
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    void MovementAndJumping()
    {
        Vector3 moveInput = Vector3.zero;
        Vector3 forward = Quaternion.AngleAxis(-90, Vector3.up) * mainCamera.transform.right;
        moveInput += forward * Input.GetAxis("Vertical");
        moveInput += mainCamera.transform.right * Input.GetAxis("Horizontal");
        moveInput.Normalize();
        Moving(moveInput.normalized, speed);

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }
}
