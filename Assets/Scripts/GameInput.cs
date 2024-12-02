using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { private set; get; }

    public event EventHandler OnPayerJump;
    public event EventHandler OnInteractComputer;
    public event EventHandler OnExitInteract;
    public event EventHandler OnGamePause;

    PlayerInput playerInput;
    void Awake()
    {
        playerInput =  new PlayerInput();
        playerInput.OnFoot.Enable();

        playerInput.OnFoot.Jump.performed += OnPlayerJump_Perfrom;
        playerInput.OnFoot.Interact.performed += OnInteractComputer_Perform;
        playerInput.OnFoot.InteractAlternative.performed += OnExitInteract_Perform;

        playerInput.OnFoot.GamePause.performed += OnGamePause_Perfrom;
        
    }

    private void OnGamePause_Perfrom(InputAction.CallbackContext context)
    {
        OnGamePause?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {
        instance = this;
    }

    public void ForceExitInteract()
    {
        OnExitInteract?.Invoke(this, EventArgs.Empty);
    }

    private void OnExitInteract_Perform(InputAction.CallbackContext context)
    {
        OnExitInteract?.Invoke(this, EventArgs.Empty); 
    }

    private void OnInteractComputer_Perform(InputAction.CallbackContext context)
    {
        OnInteractComputer?.Invoke(this, EventArgs.Empty);
    }

    private void OnPlayerJump_Perfrom(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnPayerJump?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementNormalized()
    {
        Vector2 inputVector = playerInput.OnFoot.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public Vector2 GetMouseMovement()
    { 
        Vector2 mouseInputVector = playerInput.OnFoot.Look.ReadValue<Vector2>();
        return mouseInputVector;
    }
}
