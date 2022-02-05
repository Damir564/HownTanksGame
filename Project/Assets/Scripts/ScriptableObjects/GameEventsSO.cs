using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/GameEventSO", order = 4)]
public class GameEventsSO : SingletonScriptableObject<GameEventsSO>
{
    public event UnityAction<string, string, BulletSO> BulletPlayerHitEvent;
    public event UnityAction<string> RepairingEvent;
    public event UnityAction<int> WeaponChangeEvent;

    public void RaiseBulletPlayerHitEvent(in string playerName, in string enemyName, in BulletSO enemyBullet)
    {
        BulletPlayerHitEvent?.Invoke(playerName, enemyName, enemyBullet);
    }

    public void RaiseRepairingEvent(in string playerName)
    {
        RepairingEvent?.Invoke(playerName);
    }

    public void RaiseWeaponChangeEvent(in int weaponId)
    {
        WeaponChangeEvent?.Invoke(weaponId);
    }
}
