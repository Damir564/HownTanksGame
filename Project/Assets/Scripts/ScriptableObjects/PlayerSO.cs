using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSO", order = 1)]
public class PlayerSO : ScriptableObject
{
    // PlayerSO
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private float m_rotationSpeedBody;
    [SerializeField] private float m_rotationSpeedHead;
    [SerializeField] private float m_shootZone;
    [SerializeField] private int m_totalHealth;

    public float MovementSpeed
    {
        get => m_movementSpeed;
    }
    public float RotationSpeedBody
    {
        get => m_rotationSpeedBody;
    }
    public float RotationSpeedHead
    {
        get => m_rotationSpeedHead;
    }
    public float ShootZone
    {
        get => m_shootZone;
    }

    public int TotalHealth
    {
        get => m_totalHealth;
    }
}
