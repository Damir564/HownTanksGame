using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSO", order = 1)]
public class PlayerSO : ScriptableObject, GameInput.IPlayerActions
{
    // PlayerEvents
    public event UnityAction<int> HealthChangedEvent;
    public event UnityAction<string> AmmoChangedEvent;
    public event UnityAction ShootingEvent;
    public event UnityAction<int> ScopeChangedEvent;
    public event UnityAction<Color, Transform> WeaponImageAndCameraFollowChangeEvent; // change Color to Image when sprites are ready

    public void RaiseHealthChangedEvent(in int value)
    {
        HealthChangedEvent?.Invoke(value);
    }
    public void RaiseAmmoChangedEvent(in string value)
    {
        AmmoChangedEvent?.Invoke(value);
    }
    public void RaiseShootingEvent()
    {
        ShootingEvent?.Invoke();
    }
    public void RaiseScopeChangedEvent(in int value)
    {
        ScopeChangedEvent?.Invoke(value);
    }
    public void RaiseWeaponImageAndCameraFollowChangeEvent(in Color image, in Transform cameraFollowTransform)
    {
        WeaponImageAndCameraFollowChangeEvent?.Invoke(image, cameraFollowTransform);
    }


    // Input
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
