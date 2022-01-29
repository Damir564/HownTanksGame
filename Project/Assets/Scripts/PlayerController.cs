using UnityEngine;   // TO-DO: Make Shooting with Action Type Button and add to stick if its possible. And make shooting button
using System.Collections;
using TMPro;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    // Game
    [SerializeField] private Transform allBulletsParent;

    // Input
    [SerializeField] private PlayerSO m_playerValues;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private GameObject m_body;
    private GameObject m_head;
    private Vector2 m_movement;
    private Vector2 m_aiming;
    private bool m_isMovePressed = false;
    private bool m_isAimingPressed = false;

    // UI & Cameras
    [SerializeField] private TMP_Text m_ammoCounter;
    [SerializeField] private TMP_Text m_healthCounter;
    [SerializeField] private GameObject m_weaponImageOuput;
    [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
    private UnityEngine.U2D.PixelPerfectCamera m_pixelPerfect;
    private Transform m_bulletExit;
    private AudioSource m_audioSource;

    // Shooting
    private WeaponSO m_weaponValues;
    private bool m_isNotReloading = true;
    private int m_scopingState = 0;
    private int m_currentWeaponIndex;
    private int m_currentAllAmmo;
    private int m_currentAmmo;
    private float m_nextFireTime = 0f;

    // Health & Damage
    private BulletSO m_enemyBulletValues;
    private int m_currentHealth;
    private string m_enemyName;

    private void Awake()
    {
        m_currentWeaponIndex = PlayerSO.DEFAULT_WEAPON;
        m_pixelPerfect = this.transform.Find("Main Camera").GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
        weaponChange(m_currentWeaponIndex);
        ChangeHealth(m_playerValues.TotalHealth);
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
        if (m_isNotReloading && m_currentAmmo != m_weaponValues.WeaponTotalAmmo && m_currentAllAmmo != 0)
        {
            StartCoroutine(Reloading());
            m_isNotReloading = false;
        }
    }

    private void ScopingPerformedEvent()
    {
        if (m_scopingState < m_weaponValues.WeaponScope.Length - 1){
            m_scopingState += 1;
        }
        else{
            m_scopingState = 0;
        }
        Debug.Log(m_scopingState + "!!");
        m_pixelPerfect.assetsPPU = m_weaponValues.WeaponScope[m_scopingState];
    }

    private void OnEnable()
    {
        // Input
        m_playerValues.m_MovementPerformedEvent += MovementPerformedEvent;
        m_playerValues.m_MovementCanceledEvent += MovementCanceledEvent;
        m_playerValues.m_AimingPerformedEvent += AimingPerformedEvent;
        m_playerValues.m_AimingCanceledEvent += AimingCanceledEvent;
        m_playerValues.m_ReloadingPerformedEvent += ReloadingPerformedEvent;
        m_playerValues.m_ReloadingPerformedEvent += ScopingPerformedEvent;
        // m_playerValues.m_reloadingCanceledEvent += ReloadingCanceledEvent;
    }
    private void OnDisable()
    {
        // Input
        m_playerValues.m_MovementPerformedEvent -= MovementPerformedEvent;
        m_playerValues.m_MovementCanceledEvent -= MovementCanceledEvent;
        m_playerValues.m_AimingPerformedEvent -= AimingPerformedEvent;
        m_playerValues.m_AimingCanceledEvent -= AimingCanceledEvent;
        m_playerValues.m_ReloadingPerformedEvent -= ReloadingPerformedEvent;
        m_playerValues.m_ReloadingPerformedEvent -= ScopingPerformedEvent;
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
        if (m_aiming.sqrMagnitude >= m_playerValues.ShootZone)
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        if (!m_isNotReloading || m_currentAmmo == 0 || Time.time < m_nextFireTime)
            return;
        m_nextFireTime = Time.time + m_weaponValues.WeaponFireRate;
        GameObject bullet = Instantiate(m_weaponValues.BulletPrefab, m_bulletExit.position, m_bulletExit.rotation, allBulletsParent);
        bullet.name = "Player";
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(m_bulletExit.up * m_weaponValues.BulletForce, ForceMode2D.Impulse);
        m_audioSource.PlayOneShot(m_weaponValues.WeaponSoundShoot);
        // Destroy(bullet, m_weaponValues.BulletDestroyTime);
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
        m_head = Instantiate(m_weaponValues.HeadPrefab, transform);
        m_bulletExit = m_head.transform.GetChild(0);
        m_audioSource = m_head.GetComponent<AudioSource>();
        m_currentAllAmmo = m_weaponValues.WeaponAllTotalAmmo;
        m_currentAmmo = m_weaponValues.WeaponTotalAmmo;
        m_weaponImageOuput.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 0, 0, 50);
        m_pixelPerfect.assetsPPU = m_weaponValues.WeaponScope[0];
        AmmoCounterUpdate();
    }

    private void AmmoCounterUpdate()
    {
        m_ammoCounter.text = m_currentAmmo.ToString() + "/" + m_currentAllAmmo.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            m_enemyName = collision.gameObject.name;
            m_enemyBulletValues = collision.gameObject.GetComponent<BulletsHit>().BulletValues;
            ChangeHealth(-m_enemyBulletValues.BulletDamage);
        }
    }


    private void ChangeHealth(int amount)
    {
        m_currentHealth += amount;
        if (m_currentHealth <= 0)
        {
            Debug.Log("Tank Exploded");
        }
        HealthCounterUpdate();
    }

    private void HealthCounterUpdate()
    {
        m_healthCounter.text = m_currentHealth.ToString();
    }
}
