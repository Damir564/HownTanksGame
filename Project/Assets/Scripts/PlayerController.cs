using UnityEngine;   // TO-DO: Make Shooting with Action Type Button and add to stick if its possible. And make shooting button
using System.Collections;
public class PlayerController : MonoBehaviour
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
    private bool m_notReloading = true;
    private int m_magazineBullets;
    private Vector2 m_movement;         //Movement Axis
    private Vector2 m_aiming;           //Aiming Axis

    private float m_nextFireTime = 0f;
    [SerializeField]
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_weaponValues = m_playerValues.WeaponSOs[PlayerSO.DEFAULT_WEAPON];
        m_magazineBullets = m_weaponValues.WeaponMagazineBullets;
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

    private void ReloadingPerformedEvent()
    {
        if (m_notReloading && m_magazineBullets != m_weaponValues.WeaponMagazineBullets){
            StartCoroutine(Reloading());
            m_notReloading = false;
        }
    }

    private void OnEnable()
    {
        m_playerValues.m_movementPerformedEvent += MovementPerformedEvent;
        m_playerValues.m_movementCanceledEvent += MovementCanceledEvent;
        m_playerValues.m_aimingPerformedEvent += AimingPerformedEvent;
        m_playerValues.m_aimingCanceledEvent += AimingCanceledEvent;
        m_playerValues.m_reloadingPerformedEvent += ReloadingPerformedEvent;
        // m_playerValues.m_reloadingCanceledEvent += ReloadingCanceledEvent;
    }
    private void OnDisable()
    {
        m_playerValues.m_movementPerformedEvent -= MovementPerformedEvent;
        m_playerValues.m_movementCanceledEvent -= MovementCanceledEvent;
        m_playerValues.m_aimingPerformedEvent -= AimingPerformedEvent;
        m_playerValues.m_aimingCanceledEvent -= AimingCanceledEvent;
        m_playerValues.m_reloadingPerformedEvent -= ReloadingPerformedEvent;
        // m_playerValues.m_reloadingCanceledEvent -= ReloadingCanceledEvent;
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
        if (m_magazineBullets != 0 && m_aiming.sqrMagnitude >= m_playerValues.ShootZone)
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        if (Time.time < m_nextFireTime)
            return;
        m_nextFireTime = Time.time + m_weaponValues.WeaponFireRate;
        m_audioSource.PlayOneShot(m_weaponValues.BulletSoundShoot);
        GameObject bullet = Instantiate(m_weaponValues.BulletPrefab, m_bulletExit.position, m_bulletExit.rotation);
        Rigidbody2D bulletRb = bullet.GetComponentInChildren<Rigidbody2D>();
        bulletRb.AddForce(m_bulletExit.up * m_weaponValues.BulletForce, ForceMode2D.Impulse);
        Destroy(bullet, m_weaponValues.BulletDestroyTime);
        m_magazineBullets -= 1;
        Debug.Log("Shot, ammo: " + m_magazineBullets);
    }

    // private void Reloading()
    // {
    //     m_notReloading = false;
    //     m_endReloadTime = Time.time + m_weaponValues.WeaponReloadTime;
    //     Debug.Log("Play Reloading Sound at once and add bullets");
    //     while (Time.time < m_endReloadTime)
    //     {
    //         break;
    //     }
    //     m_notReloading = true;
    //     Debug.Log("Tipo Reloaded)");
    // }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(m_weaponValues.WeaponReloadTime);
        m_magazineBullets = m_weaponValues.WeaponMagazineBullets;
        Debug.Log("Reloaded, ammo: " + m_magazineBullets);
        m_notReloading = true;
    }
}
