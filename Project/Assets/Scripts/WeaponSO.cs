using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponSO", order = 2)]
public class WeaponSO : ScriptableObject
{
    //Prefabs:
    [SerializeField]
    private GameObject m_bulletPrefab;
    [SerializeField]
    private GameObject m_headPrefab;
    //Bullet Properites:
    [SerializeField]
    private float m_bulletForce;
    [SerializeField]
    private float m_bulletDestroyTime;
    [SerializeField]
    private AudioClip m_bulletSoundShoot;
    //Weapon Properties:
    [SerializeField]
    private float m_weaponFireRate;
    [SerializeField]
    private float m_weaponReloadTime;
    [SerializeField]
    private int m_weaponAmmo;
    [SerializeField]
    private int m_weaponTotalAmmo;
    [SerializeField]
    private int m_weaponCurrentAmmo;
    //Conditions
    [SerializeField]
    private bool m_weaponNotReloading;


    public GameObject BulletPrefab
    {
        get => m_bulletPrefab;
    }
    public GameObject HeadPrefab
    {
        get => m_headPrefab;
    }
    public float BulletForce
    {
        get => m_bulletForce;
    }
    public float BulletDestroyTime
    {
        get => m_bulletDestroyTime;
    }
    public AudioClip BulletSoundShoot
    {
        get => m_bulletSoundShoot;
    }
    public float WeaponFireRate
    {
        get => m_weaponFireRate;
    }
    public float WeaponReloadTime
    {
        get => m_weaponReloadTime;
    }
    public int WeaponAmmo
    {
        get => m_weaponAmmo;
    }
    public int WeaponTotalAmmo
    {
        get => m_weaponTotalAmmo;
    }
    public int WeaponCurrentAmmo
    {
        get => m_weaponCurrentAmmo;
        set => m_weaponCurrentAmmo = value;
    }
    public bool WeaponNotReloading
    {
        get => m_weaponNotReloading;
        set => m_weaponNotReloading = value;
    }
}
