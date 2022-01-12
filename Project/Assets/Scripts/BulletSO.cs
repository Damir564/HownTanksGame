using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BulletSO", order = 2)]
public class BulletSO : ScriptableObject
{
    private GameObject m_prefab;
    public GameObject Prefab
    {
        get => m_prefab;
    }

    private float m_bulletForce;
    public float BulletForce
    {
        get => m_bulletForce;
    }

    private float m_bulletDestroyTime;
    public float BulletDestroyTime
    {
        get => m_bulletDestroyTime;
    }
}
