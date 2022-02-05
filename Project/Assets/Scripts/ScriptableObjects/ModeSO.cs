using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ModeSO", order = 7)]
public class ModeSO : ScriptableObject
{
    public enum Weapons
    {
        Simple,
        Shotgun,
        Sniper,
        Rapid,
        Machinegun
    }

    [SerializeField] private int m_startWeaponIndex;

    public int StartWeaponIndex
    {
        get => m_startWeaponIndex;
    }

    [SerializeField]
    private WeaponSO[] m_weaponSOs;

    public WeaponSO[] WeaponSOs
    {
        get => m_weaponSOs;
    }

}
