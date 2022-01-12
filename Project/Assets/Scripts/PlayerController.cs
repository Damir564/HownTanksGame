using UnityEngine;
using UnityEngine.InputSystem;

//Carry out do we need a FixedUpdate() or just can use Update()  <-- to-do thing
public class PlayerController : MonoBehaviour//, PlayerInputActions.IPlayerActions
{
    [SerializeField]
    private PlayerSO playerValues;

    [SerializeField]
    private float m_movementSpeed;
    [SerializeField]
    private float m_rotationSpeedBody;
    [SerializeField]
    private float m_rotationSpeedHead;
    [SerializeField]
    private float m_shootZone;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private GameObject head;

    [SerializeField]
    private float m_bulletDestroyTime;
    // [SerializeField]
    // private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletExit;
    [SerializeField]
    private float m_bulletForce;

    private PlayerInputActions playerInputAction;
    private bool m_isMovePressed = false;
    private bool m_isAimingPressed = false;
    private Vector2 m_movement;         //Movement Axis
    private Vector2 m_aiming;           //Aiming Axis

    private void Awake()
    {
        playerInputAction = new PlayerInputActions();

        playerInputAction.Player.Movement.performed += ctx => MovementEvent(ctx.ReadValue<Vector2>());
        playerInputAction.Player.Movement.canceled += _ => m_isMovePressed = false;

        playerInputAction.Player.Aiming.performed += ctx => AimingEvent(ctx.ReadValue<Vector2>());
        playerInputAction.Player.Aiming.canceled += _ => m_isAimingPressed = false;


        // PC
        playerInputAction.Player.Movement2.performed += ctx => MovementEvent(ctx.ReadValue<Vector2>());
        playerInputAction.Player.Movement2.canceled += _ => m_isMovePressed = false;
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
        playerInputAction.Enable();
    }
    private void OnDisable()
    {
        playerInputAction.Disable();
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
        rb.MovePosition(rb.position + m_movement * m_movementSpeed * Time.fixedDeltaTime);
        Quaternion toBodyRotation = Quaternion.LookRotation(Vector3.forward, m_movement);
        body.transform.rotation = Quaternion.RotateTowards(body.transform.rotation, toBodyRotation, Time.fixedDeltaTime * m_rotationSpeedBody);
    }

    private void AimingHandler()
    {
        Quaternion toHeadRotation = Quaternion.LookRotation(Vector3.forward, m_aiming);
        head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, toHeadRotation, Time.fixedDeltaTime * m_rotationSpeedHead);
        if (m_aiming.sqrMagnitude >= m_shootZone)
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        // GameObject bullet = Instantiate(bulletPrefab, bulletExit.position, bulletExit.rotation);
        // GameObject bullet = Instantiate(playerValues.bulletPrefab, bulletExit.position, bulletExit.rotation);
        // Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        // bulletRb.AddForce(bulletExit.up * m_bulletForce, ForceMode2D.Impulse);
        // Destroy(bullet, m_bulletDestroyTime);
    }

}
