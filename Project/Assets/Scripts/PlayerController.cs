using UnityEngine;
using UnityEngine.InputSystem;

//Carry out do we need a FixedUpdate() or just can use Update()  <-- to-do thing
public class PlayerController : MonoBehaviour//, PlayerInputActions.IPlayerActions
{
    [SerializeField]
    private PlayerSO m_playerValues;
    private WeaponSO m_weaponValues;

    private Rigidbody2D m_rb;
    [SerializeField]
    private GameObject m_body;
    [SerializeField]
    private GameObject m_head;
    [SerializeField]
    private Transform m_bulletExit;

    private PlayerInputActions m_playerInputAction;
    private bool m_isMovePressed = false;
    private bool m_isAimingPressed = false;
    private Vector2 m_movement;         //Movement Axis
    private Vector2 m_aiming;           //Aiming Axis

    private void Awake()
    {
        m_playerInputAction = new PlayerInputActions();

        m_playerInputAction.Player.Movement.performed += ctx => MovementEvent(ctx.ReadValue<Vector2>());
        m_playerInputAction.Player.Movement.canceled += _ => m_isMovePressed = false;

        m_playerInputAction.Player.Aiming.performed += ctx => AimingEvent(ctx.ReadValue<Vector2>());
        m_playerInputAction.Player.Aiming.canceled += _ => m_isAimingPressed = false;


        // PC
        m_playerInputAction.Player.Movement2.performed += ctx => MovementEvent(ctx.ReadValue<Vector2>());
        m_playerInputAction.Player.Movement2.canceled += _ => m_isMovePressed = false;

        m_weaponValues = m_playerValues.WeaponSOs[m_playerValues.DEFAULT_WEAPON];
        m_rb = GetComponent<Rigidbody2D>();
    }

    // public void OnMovement(InputAction.CallbackContext ctx)
    // {

    // }

    // public void OnAiming(InputAction.CallbackContext ctx)
    // {

    // }

    // public void OnMovement2(InputAction.CallbackContext ctx)
    // {

    // }




    private void MovementEvent(Vector2 value)
    {
        m_movement = value;
        m_isMovePressed = true;
    }

    private void AimingEvent(Vector2 value)
    {
        m_aiming = value;
        m_isAimingPressed = true;
    }

    private void OnEnable()
    {
        m_playerInputAction.Enable();
    }
    private void OnDisable()
    {
        m_playerInputAction.Disable();
    }

    void FixedUpdate()
    {
        if (m_isMovePressed)
        {
            MovementHandler();
        }
        if (m_isAimingPressed)
        {
            AimingHandler();
        }
    }

    private void MovementHandler()
    {
        m_rb.MovePosition(m_rb.position + m_movement * m_playerValues.MovementSpeed * Time.fixedDeltaTime);
        Quaternion toBodyRotation = Quaternion.LookRotation(Vector3.forward, m_movement);
        m_body.transform.rotation = Quaternion.RotateTowards(m_body.transform.rotation, toBodyRotation, Time.fixedDeltaTime * m_playerValues.RotationSpeedBody);
    }

    private void AimingHandler()
    {
        Quaternion toHeadRotation = Quaternion.LookRotation(Vector3.forward, m_aiming);
        m_head.transform.rotation = Quaternion.RotateTowards(m_head.transform.rotation, toHeadRotation, Time.fixedDeltaTime * m_playerValues.RotationSpeedHead);
        if (m_aiming.sqrMagnitude >= m_playerValues.ShootZone)
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        // GameObject bullet = Instantiate(bulletPrefab, bulletExit.position, bulletExit.rotation);
        GameObject bullet = Instantiate(m_weaponValues.BulletPrefab, m_bulletExit.position, m_bulletExit.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(m_bulletExit.up * m_weaponValues.BulletForce, ForceMode2D.Impulse);
        Destroy(bullet, m_weaponValues.BulletDestroyTime);
    }

}
