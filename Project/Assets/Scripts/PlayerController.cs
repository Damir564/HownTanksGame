using UnityEngine;   // TO-DO: Make Shooting with Action Type Button and add to stick if its possible. And make shooting button
using System.Collections;
using TMPro;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerSO m_playerValues; // = default   ???
    private WeaponSO m_weaponValues;

    [SerializeField]
    private TextMeshProUGUI m_ammoCounter;
    [SerializeField]
    private TextMeshProUGUI m_healthCounter;
    [SerializeField]
    private GameObject m_weaponImageOuput;

    [SerializeField]
    private Rigidbody2D m_rb;
    [SerializeField]
    private GameObject m_body;
    private GameObject m_head;
    private Transform m_bulletExit;
    [SerializeField]
    private CinemachineVirtualCamera m_virtualCamera;
    private int m_currentHealth;



    private bool m_isMovePressed = false;
    private bool m_isAimingPressed = false;
    private bool m_isNotReloading = true;
    private int m_currentAllAmmo;
    private int m_currentAmmo;
    private Vector2 m_movement;         //Movement Axis
    private Vector2 m_aiming;           //Aiming Axis

    private float m_nextFireTime = 0f;
    [SerializeField]
    private AudioSource m_audioSource;

    private void Awake()
    {
        weaponChange(PlayerSO.DEFAULT_WEAPON);
        m_currentHealth = m_playerValues.TotalHealth;
        HealthCounterUpdate(0);
        m_virtualCamera.Follow = m_head.transform.Find("camerafollow");
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
        if (m_isNotReloading && m_currentAmmo != m_weaponValues.WeaponTotalAmmo && m_currentAllAmmo != 0){
            StartCoroutine(Reloading());
            m_isNotReloading = false;
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
        if (m_isNotReloading && m_currentAmmo != 0 && m_aiming.sqrMagnitude >= m_playerValues.ShootZone)
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        if (Time.time < m_nextFireTime)
            return;
        m_nextFireTime = Time.time + m_weaponValues.WeaponFireRate;
        GameObject bullet = Instantiate(m_weaponValues.BulletPrefab, m_bulletExit.position, m_bulletExit.rotation);
        Rigidbody2D bulletRb = bullet.GetComponentInChildren<Rigidbody2D>();
        bulletRb.AddForce(m_bulletExit.up * m_weaponValues.BulletForce, ForceMode2D.Impulse);
        m_audioSource.PlayOneShot(m_weaponValues.BulletSoundShoot);
        Destroy(bullet, m_weaponValues.BulletDestroyTime);
        m_currentAmmo -= 1;
        AmmoCounterUpdate();
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(m_weaponValues.WeaponReloadTime);
        if (m_currentAllAmmo + m_currentAmmo > m_weaponValues.WeaponTotalAmmo)
        {
            m_currentAllAmmo -= m_weaponValues.WeaponTotalAmmo - m_currentAmmo;
            m_currentAmmo = m_weaponValues.WeaponTotalAmmo;
        }
        else
        {
            m_currentAmmo = m_currentAllAmmo + m_currentAmmo;
            m_currentAllAmmo = 0;
        }
        m_isNotReloading = true;
        AmmoCounterUpdate();
    }

    private void weaponChange(int weaponid)
    {
        m_weaponValues = m_playerValues.WeaponSOs[weaponid];
        m_head = Instantiate(m_weaponValues.HeadPrefab, this.transform);
        m_bulletExit = m_head.transform.GetChild(0);
        m_audioSource = m_head.GetComponent<AudioSource>();
        m_currentAllAmmo = m_weaponValues.WeaponAllTotalAmmo;
        m_currentAmmo = m_weaponValues.WeaponTotalAmmo;
        m_weaponImageOuput.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 0, 0, 50);  // then get sprite from weaponSO  .sprite not .color
        AmmoCounterUpdate();
    }

    private void AmmoCounterUpdate()
    {
        m_ammoCounter.text = m_currentAmmo.ToString() + "/" + m_currentAllAmmo.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if will be needed: audioclip by bullet tag
        if (collision.gameObject.tag == "Projectile")
        {
            Vector3 soundPos = collision.transform.position;
            collision.gameObject.SetActive(false);
            Transform bulletParent = collision.transform.parent;
            collision.transform.parent.GetChild(1).gameObject.SetActive(true);
            Destroy(bulletParent.gameObject, 0.2f);
            HealthCounterUpdate(20);
        }
    }

    private void HealthCounterUpdate(int damage)
    {
        m_currentHealth -= damage;
        m_healthCounter.text = m_currentHealth.ToString();
        if (m_currentHealth <= 0)
        {
            Debug.Log("Tank Exploded");
        }
    }
}
