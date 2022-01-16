using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponSO", order = 2)]
public class WeaponSO : ScriptableObject
{
    [SerializeField]
    private GameObject m_bulletPrefab;
    [SerializeField]
    private float m_bulletForce;
    [SerializeField]
    private float m_bulletDestroyTime;
    // [SerializeField]
    // private AudioClip m_shootAudioClip;
    // [SerializeField]
    // private AudioConfigurationSO m_shootAudioConfiguration;
    public AudioSource au;
    public AudioClip auclp;
    [SerializeField]
    public SoundEmitter soundEmitter;

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
    // public AudioClip ShootAudioClip
    // {
    //     get => m_shootAudioClip;
    // }
    // public AudioConfigurationSO ShootAudioConfiguration
    // {
    //     get => m_shootAudioConfiguration;
    // }
    public void BulletShootSound()
    {
        au.PlayOneShot(auclp);
    }
}
