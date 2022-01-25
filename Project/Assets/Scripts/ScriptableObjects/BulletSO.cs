using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BulletSO", order = 3)]
public class BulletSO : ScriptableObject
{
    [SerializeField] private AudioClip m_bulletSoundHit;
    [SerializeField] private float m_bulletDestroyTime;
    [SerializeField] private float m_bulletSoundHitTime;
    [SerializeField] private int m_bulletDamage;

    public AudioClip BulletSoundHit
    {
        get => m_bulletSoundHit;
    }
    public float BulletDestroyTime
    {
        get => m_bulletDestroyTime;
    }
    public float BulletSountHitTime
    {
        get => m_bulletSoundHitTime;
    }
    public int BulletDamage
    {
        get => m_bulletDamage;
    }
}
