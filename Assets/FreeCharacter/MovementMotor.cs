using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMotor : MonoBehaviour
{
    public float gravityMultiplier = 1f;
    public float lerpTime = 10f;
    public Vector3 moveDirection = Vector3.zero;
    private Vector3 targetDirection = Vector3.zero;
    private float fallVelocity = 0f;
    [HideInInspector]
    public CharacterController charController;
    public float distanceToGround = 0.1f;
    [HideInInspector]
    public bool isGrounded;
    private int jumpcount;
    private bool candoublejump;


    void Awake()
    {
        charController = GetComponent<CharacterController>();
        jumpcount = 0;
    }

    void Update()
    {
            isGrounded = OnGroundCheck();
            moveDirection = Vector3.Lerp(moveDirection, targetDirection, Time.deltaTime * lerpTime);
            moveDirection.y = fallVelocity;
            charController.Move(moveDirection * Time.deltaTime);
        if (!isGrounded)
        {
            fallVelocity -= 90f * gravityMultiplier * Time.deltaTime;
        }
    }

    public bool OnGroundCheck()
    {
        if (charController.isGrounded)
        {
            jumpcount = 0;
            return true;
        }
        return false;
    }

    public void Move(Vector3 dir)
    {
        targetDirection = dir;
    }
    public void Stop()
    {
        moveDirection = Vector3.zero;
        targetDirection = Vector3.zero;
    }
    public void Jump(float jumpSpeed)
    {
        if (isGrounded)
        {
            fallVelocity = jumpSpeed;
            candoublejump = true;
        }
        else
        {
            if(candoublejump == true && Input.GetKeyDown(KeyCode.Space))
            {
                fallVelocity = jumpSpeed;
                candoublejump = false;
                jumpcount++;
            }
        }
    }

}
