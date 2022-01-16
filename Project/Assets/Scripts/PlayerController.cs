using UnityEngine;
using UnityEngine.InputSystem;


//Carry out do we need a FixedUpdate() or just can use Update()  <-- to-do thing
public class PlayerController : MonoBehaviour//, PlayerInputActions.IPlayerActions
{
    [SerializeField]
    private PlayerSO m_playerValues; // = default   ???
    private WeaponSO m_weaponValues;

    private Rigidbody2D m_rb;
    [SerializeField]
    private GameObject m_body;
    [SerializeField]
    private GameObject m_head;
    [SerializeField]
    private Transform m_bulletExit;

    private bool m_isMovePressed = false;
    private bool m_isAimingPressed = false;
    private Vector2 m_movement;         //Movement Axis
    private Vector2 m_aiming;           //Aiming Axis

    private void Awake()
    {
        m_weaponValues = m_playerValues.WeaponSOs[PlayerSO.DEFAULT_WEAPON];
        m_rb = GetComponent<Rigidbody2D>();
    }




    private void MovementPerformedEvent(Vector2 value)
    {
        m_movement = value;
        m_isMovePressed = true;
    }

    private void MovementCanceledEvent()
    {
        m_isMovePressed = false;
    }

    private void AimingPerformedEvent(Vector2 value)
    {
        m_aiming = value;
        m_isAimingPressed = true;
    }

    private void AimingCanceledEvent()
    {
        m_isAimingPressed = false;
    }

    private void OnEnable()
    {
        m_playerValues.m_movementPerformedEvent += MovementPerformedEvent;
        m_playerValues.m_movementCanceledEvent += MovementCanceledEvent;
        m_playerValues.m_aimingPerformedEvent += AimingPerformedEvent;
        m_playerValues.m_aimingCanceledEvent += AimingCanceledEvent;
    }
    private void OnDisable()
    {
        m_playerValues.m_movementPerformedEvent -= MovementPerformedEvent;
        m_playerValues.m_movementCanceledEvent -= MovementCanceledEvent;
        m_playerValues.m_aimingPerformedEvent -= AimingPerformedEvent;
        m_playerValues.m_aimingCanceledEvent -= AimingCanceledEvent;
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
        // m_weaponValues.BulletShootSound();
        //m_weaponValues.BulletShootSound.Play();
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(m_bulletExit.up * m_weaponValues.BulletForce, ForceMode2D.Impulse);
        Destroy(bullet, m_weaponValues.BulletDestroyTime);
    }

}
