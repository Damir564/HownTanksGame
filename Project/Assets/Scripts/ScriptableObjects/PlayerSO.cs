using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSO", order = 1)]
public class PlayerSO : ScriptableObject, GameInput.IPlayerActions
{
    // Input
    public event UnityAction<Vector2> m_MovementPerformedEvent;
    public event UnityAction m_MovementCanceledEvent;
    public event UnityAction<Vector2> m_AimingPerformedEvent;
    public event UnityAction m_AimingCanceledEvent;
    public event UnityAction m_ReloadingPerformedEvent;
    public event UnityAction m_ScopingPerformedEvent;

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
        if (m_MovementPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            m_MovementPerformedEvent.Invoke(ctx.ReadValue<Vector2>());
        if (m_MovementCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
            m_MovementCanceledEvent.Invoke();
    }

    ///***
    /// PC, has to be deleted in build
    ///***
    public void OnMovement2(InputAction.CallbackContext ctx)
    {
        if (m_MovementPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            m_MovementPerformedEvent.Invoke(ctx.ReadValue<Vector2>());
        if (m_MovementCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
            m_MovementCanceledEvent.Invoke();
    }

    public void OnAiming(InputAction.CallbackContext ctx)
    {
        if (m_AimingPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            m_AimingPerformedEvent.Invoke(ctx.ReadValue<Vector2>());
        if (m_AimingCanceledEvent != null && ctx.phase == InputActionPhase.Canceled)
            m_AimingCanceledEvent.Invoke();
    }

    public void OnReloading(InputAction.CallbackContext ctx)
    {
        if (m_ReloadingPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            Debug.Log("Reloading");
        m_ReloadingPerformedEvent.Invoke();
    }

    public void OnScoping(InputAction.CallbackContext ctx)
    {
        if (m_ScopingPerformedEvent != null && ctx.phase == InputActionPhase.Performed)
            Debug.Log("Scoping");
        m_ScopingPerformedEvent.Invoke();
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
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private float m_rotationSpeedBody;
    [SerializeField] private float m_rotationSpeedHead;
    [SerializeField] private float m_shootZone;

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

    // Player
    [SerializeField] private int m_totalHealth;
    public int TotalHealth
    {
        get => m_totalHealth;
    }
}
