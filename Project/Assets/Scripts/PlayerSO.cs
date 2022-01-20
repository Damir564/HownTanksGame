using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSO", order = 1)]
public class PlayerSO : ScriptableObject, GameInput.IPlayerActions
{
    // Input
    public event UnityAction<Vector2> m_movementPerformedEvent;
    public event UnityAction m_movementCanceledEvent;
    public event UnityAction<Vector2> m_aimingPerformedEvent;
    public event UnityAction m_aimingCanceledEvent;
    public event UnityAction m_reloadingPerformedEvent;
    // public event UnityAction m_reloadingCanceledEvent;

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
        if (m_movementPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            m_movementPerformedEvent.Invoke(ctx.ReadValue<Vector2>());
        if (m_movementCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
            m_movementCanceledEvent.Invoke();
    }

    ///***
    /// PC, has to be deleted in build
    ///***
    public void OnMovement2(InputAction.CallbackContext ctx)
    {
        if (m_movementPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            m_movementPerformedEvent.Invoke(ctx.ReadValue<Vector2>());
        if (m_movementCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
            m_movementCanceledEvent.Invoke();
    }

    public void OnAiming(InputAction.CallbackContext ctx)
    {
        if (m_aimingPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            m_aimingPerformedEvent.Invoke(ctx.ReadValue<Vector2>());
        if (m_aimingCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
            m_aimingCanceledEvent.Invoke();
    }

    public void OnReloading(InputAction.CallbackContext ctx)
    {
        if (m_reloadingPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            m_reloadingPerformedEvent.Invoke();
        // if (m_reloadingCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
        //     m_reloadingCanceledEvent.Invoke();
    }

    // Weapon
    public enum Weapons
    {
        Simple,
        Shotgun,
        Sniper,
        Rapid,
        Machinegun
    }

    [HideInInspector]
    public const int DEFAULT_WEAPON = (int)Weapons.Simple;

    [SerializeField]
    private WeaponSO[] m_weaponSOs = new WeaponSO[5];

    public WeaponSO[] WeaponSOs
    {
        get => m_weaponSOs;
    }

    // Body
    [SerializeField]
    private float m_movementSpeed;
    [SerializeField]
    private float m_rotationSpeedBody;
    [SerializeField]
    private float m_rotationSpeedHead;
    [SerializeField]
    private float m_shootZone;

    public float MovementSpeed
    {
        get => m_movementSpeed;
    }
    public float RotationSpeedBody
    {
        get => m_rotationSpeedBody;
    }
    public float RotationSpeedHead
    {
        get => m_rotationSpeedHead;
    }
    public float ShootZone
    {
        get => m_shootZone;
    }
}
