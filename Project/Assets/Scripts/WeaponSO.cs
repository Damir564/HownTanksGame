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
    [SerializeField]
    private AudioSource m_bulletShootSound;

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
    public AudioSource BulletShootSound
    {
        get => m_bulletShootSound;
    }
}
