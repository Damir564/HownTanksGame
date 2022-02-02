using UnityEngine;

public class Health : MonoBehaviour, IPlayerComonent
{
    [SerializeField] PlayerController m_playerController;
    private BulletSO m_enemyBulletValues;
    private string m_enemyName;

    // private int m_currentHealth;
    // public int CurrentHealth
    // {
    //     get => m_currentHealth;
    //     set
    //     {
    //         m_currentHealth = value;
    //         if (m_currentHealth > m_playerController.PlayerValues.TotalHealth)
    //             m_currentHealth = m_playerController.PlayerValues.TotalHealth;
    //         else if (m_currentHealth <= 0)
    //         {
    //             m_currentHealth = 0;
    //             Debug.Log("Tank Exploded");
    //         }
    //         m_playerController.HealthCounterUpdate();
    //     }
    // }

    private void OnEnable()
    {
        GameEventSO.Instance.onBulletHit += OnBulletHit;
    }

    private void OnDisable()
    {
        GameEventSO.Instance.onBulletHit -= OnBulletHit;
    }

    private void OnBulletHit(string playerName, string enemyName, BulletSO enemyBullet)
    {
        if (playerName == name)
        {
            Debug.Log("selfharm");
        }
        m_playerController.CurrentHealth -= enemyBullet.BulletDamage;
    }


    public void OnAwake()
    {
        m_playerController.CurrentHealth = m_playerController.PlayerValues.TotalHealth;
    }
}
