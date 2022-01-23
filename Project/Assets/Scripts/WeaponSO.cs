using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponSO", order = 2)]
public class WeaponSO : ScriptableObject
{
    [SerializeField]
    private GameObject m_bulletPrefab;
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
    private int m_weaponMagazineBullets;


    public GameObject BulletPrefab
    {
        get => m_bulletPrefab;
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
    public int WeaponMagazineBullets
    {
        get => m_weaponMagazineBullets;
    }
}
