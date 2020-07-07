using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 
                                  0f, Input.GetAxis("Vertical") * moveSpeed);

        if(Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
