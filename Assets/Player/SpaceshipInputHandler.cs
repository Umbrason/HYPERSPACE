
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipInputHandler : MonoBehaviour, GameplayInput.ISpaceshipActions
{
    private GameplayInput input;

    public static Action<Vector2> OnMoveChanged;
    public static Action<bool> OnPrimaryFireChanged;
    public static Action<bool> OnSecondaryFireChanged;
    public static Action OnTriggerUltimate;
    public static Action OnTriggerRoll;

    void Start()
    {
        input = new();
        input.Spaceship.SetCallbacks(this);
        input.Spaceship.Enable();
    }

    public void OnMove(InputAction.CallbackContext context) => OnMoveChanged?.Invoke(context.ReadValue<Vector2>());    

    public void OnPrimaryFire(InputAction.CallbackContext context)
    {
        OnPrimaryFireChanged?.Invoke(context.ReadValue<bool>());
    }
    public void OnSecondaryFire(InputAction.CallbackContext context)
    {
        OnSecondaryFireChanged?.Invoke(context.ReadValue<bool>());
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if(context.performed) OnTriggerRoll?.Invoke();
    }


    public void OnUltimate(InputAction.CallbackContext context)
    {
        if(context.performed) OnTriggerUltimate?.Invoke();
    }
}
