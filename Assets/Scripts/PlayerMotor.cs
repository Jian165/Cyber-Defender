using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrouded;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();        
    }

    // Update is called once per frame
    void Update()
    {
        isGrouded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    { 
        Vector3 moveDirection  =  Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrouded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }   
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
        
    }

    public void Jump()
    {
        if (isGrouded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
