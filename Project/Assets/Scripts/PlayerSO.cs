using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSO", order = 1)]
public class PlayerSO : ScriptableObject
{
    public enum Weapons
    {
        Simple,
        Shotgun,
        Sniper,
        Rapid,
        Machinegun
    }

    [HideInInspector]
    public int DEFAULT_WEAPON = (int)Weapons.Simple;

    [SerializeField]
    private WeaponSO[] m_weaponSOs = new WeaponSO[5];

    public WeaponSO[] WeaponSOs
    {
        get => m_weaponSOs;
    }

    [SerializeField]
    private float m_movementSpeed;
    [SerializeField]
    private float m_rotationSpeedBody;
    [SerializeField]
    private float m_rotationSpeedHead;
    [SerializeField]
    private float m_shootZone;

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
}
