using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/InputEventsSO", order = 5)]
public class InputEventsSO : ScriptableObject, GameInput.IPlayerActions
{
    public event UnityAction<Vector2> MovementPerformedEvent;
    public event UnityAction MovementCanceledEvent;
    public event UnityAction<Vector2> AimingPerformedEvent;
    public event UnityAction AimingCanceledEvent;
    public event UnityAction ReloadingPerformedEvent;
    public event UnityAction ScopingPerformedEvent;

    private GameInput m_gameInput;

    private void OnEnable()
    {
        if (m_gameInput == null)
        {
            m_gameInput = new GameInput();
            m_gameInput.Player.SetCallbacks(this);
        }
        EnableGameInput();
    }

    private void OnDisable()
    {
        DisableGameInput();
    }

    private void EnableGameInput()
    {
        m_gameInput.Player.Enable();
    }

    private void DisableGameInput()
    {
        m_gameInput.Player.Disable();
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        if (MovementPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            MovementPerformedEvent.Invoke(ctx.ReadValue<Vector2>());
        if (MovementCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
            MovementCanceledEvent.Invoke();
    }

    ///***
    /// PC, has to be deleted in build
    ///***
    public void OnMovement2(InputAction.CallbackContext ctx)
    {
        if (MovementPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            MovementPerformedEvent.Invoke(ctx.ReadValue<Vector2>());
        if (MovementCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
            MovementCanceledEvent.Invoke();
    }

    public void OnAiming(InputAction.CallbackContext ctx)
    {
        if (AimingPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            AimingPerformedEvent.Invoke(ctx.ReadValue<Vector2>());
        if (AimingCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
            AimingCanceledEvent.Invoke();
    }

    public void OnReloading(InputAction.CallbackContext ctx)
    {
        if (ReloadingPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            Debug.Log("Reloading");
        ReloadingPerformedEvent.Invoke();
    }

    public void OnScoping(InputAction.CallbackContext ctx)
    {
        if (ScopingPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            Debug.Log("Scoping");
        ScopingPerformedEvent.Invoke();
    }
}
