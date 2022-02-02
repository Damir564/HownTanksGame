using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/GameEventSO", order = 4)]
public class GameEventSO : SingletonScriptableObject<GameEventSO>
{
    public event UnityAction<string, string, BulletSO> onBulletHit;
    public void BulletHit(string playerName, string enemyName, BulletSO enemyBullet)
    {
        if (onBulletHit != null)
        {
            onBulletHit(playerName, enemyName, enemyBullet);
        }
    }

}
