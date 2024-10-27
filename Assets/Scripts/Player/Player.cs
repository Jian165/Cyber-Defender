using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // fields for movement
    [Header("Character Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -19.6f;

    private CharacterController controller;
    private bool isGrounded;
    private Vector3 playerGravityVelocity;
    private bool isWalking;
    Vector2 inputMoveDirection ;
    private bool canMove = true;
    private float groundedGravPull = -2.0f;
    private float scalingFactor = -3f;

    // fields for camera movement
    [Header("Camera and Mouse Movement")]
    [SerializeField] Camera cam;
    [SerializeField] Transform HeadObject;

    private float xRotation = 0f;
    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;

    [SerializeField] private float minRotation = 30f;
    [SerializeField] private float maxRotation = -60f;


    // interaction field
    [Header("Interaction")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TextMeshProUGUI playerPromt;
    [SerializeField] private float intracDistance = 5f;
    [SerializeField] private GameObject playerUI;

    private Interactable selectedComputer;

    public event EventHandler<OnSelectedComputerChangeEventArgs> OnSelectedComputerChange;

    public class OnSelectedComputerChangeEventArgs : EventArgs
    {
        public Interactable selectedComputer;
    }



    private void GameInput_OnPlayeInteractComputer(object sender, EventArgs e)
    {
        if (selectedComputer != null)
        {
            selectedComputer.Interact(this);
        }
    }

    private void GameInput_OnPlayerJump(object sender, EventArgs e)
    {
        if (isGrounded)
        {
            playerGravityVelocity.y = Mathf.Sqrt(jumpHeight * scalingFactor * gravity);
        }
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        // lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        gameInput.OnPayerJump += GameInput_OnPlayerJump;
        gameInput.OnInteractComputer += GameInput_OnPlayeInteractComputer;
    }
    // Update is called once per frame
    void Update()
    {
        // check if the player is on the ground
        isGrounded = controller.isGrounded;
        PlayerMovement();
        PlayerHeadMovemen();
        PlayerInteraction();

    }

    public void PlayerMovement()
    {
        if (canMove)
        {
            // Playermovement x and y direction
            inputMoveDirection = gameInput.GetMovementNormalized();
            Vector3 moveDir = new Vector3(inputMoveDirection.x, 0f, inputMoveDirection.y);

            isWalking = moveDir != Vector3.zero;

            // performs the movement
            controller.Move(transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime); // move x and move z
            playerGravityVelocity.y += gravity * Time.deltaTime; // move y

            // Apply gravity to Player if player is t on the ground
            if (isGrounded && playerGravityVelocity.y < 0)
            {
                // if the player is on the ground it will only apply a force of 2 downward
                playerGravityVelocity.y = groundedGravPull;
            }
            controller.Move(playerGravityVelocity * Time.deltaTime);
        }
    }

    public void PlayerHeadMovemen()
    {
        if (canMove)
        {
            // mousemovement
            float mouseX = gameInput.GetMouseMovement().x;
            float mouseY = gameInput.GetMouseMovement().y;

            // x rotation of an object;
            xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
            xRotation = Mathf.Clamp(xRotation, maxRotation, minRotation); // x rotation object constraint

            // camera and head rotation
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            HeadObject.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // body rotation
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        }
        
    }

    // computer interaction
    public void PlayerInteraction()
    {

        TextPromptUpdate(string.Empty);
        // check if camera raycast hit some interactable object
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit camRaycastHit, intracDistance, layerMask))
        {
            if (camRaycastHit.transform.TryGetComponent(out Interactable interactable))
            {
                TextPromptUpdate(interactable.promtMessage); // get the promt message of the computer and display it to the player UI

                // check if the raycast select the same Interactable object if not set the object as the new selected computer 
                if (interactable != selectedComputer)
                {
                    SetSelectedInteractable(interactable);
                }
            }

            else
            {
                SetSelectedInteractable(null); // do not change the seleted interatable if the object seleted is the same
            }

        }

        else
        {
            SetSelectedInteractable(null); // if nothing is hit by the raycast set seleted Interatable to null
        }
    }

    //  corsshair text prompt
    public void TextPromptUpdate(string textPrompt)
    {
        playerPromt.text = textPrompt;
    }

    private void SetSelectedInteractable(Interactable selectedComputer)
    { 
        // set the value to the local seletedComputer varaible
        this.selectedComputer = selectedComputer;
        OnSelectedComputerChange?.Invoke(this, new OnSelectedComputerChangeEventArgs
        { 
            // set the value to the event variable
            selectedComputer = selectedComputer
        });
    }

    public void isPlayerInComputer (bool isInComputer)
    {
        // eable and disable

        // movement 
        canMove = !isInComputer;

        // Player UI
        playerUI.SetActive(!isInComputer);

        if (!isInComputer)
        {
        // if the player is not in the computer lock the cursour
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        { 
        // if the player is in the computer unlock the cursour
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public Vector2 WalikngDirection()
    {
        return inputMoveDirection; 
    }
    
}
