using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BulletSO", order = 2)]
public class WeaponSO : ScriptableObject
{
    [SerializeField]
    private GameObject m_bulletPrefab;
    [SerializeField]
    private float m_bulletForce;
    [SerializeField]
    private float m_bulletDestroyTime;

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
}
