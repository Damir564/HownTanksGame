using UnityEngine;
using System.Collections;

public class Health : PlayerModule
{
    private BulletSO m_enemyBulletValues;
    private string m_enemyName;

    private int m_currentHealth;
    public int CurrentHealth
    {
        get => m_currentHealth;
        set
        {
            m_currentHealth = value;
            if (m_currentHealth > m_playerController.PlayerValues.TotalHealth)
                m_currentHealth = m_playerController.PlayerValues.TotalHealth;
            else if (m_currentHealth <= 0)
            {
                m_currentHealth = 0;
                Debug.Log("Tank Exploded");
            }
            GameManager.Instance.PlayerEvents.RaiseHealthChangedEvent(m_currentHealth);
        }
    }

    public override void OnOnEnable()
    {
        GameManager.Instance.GameEvents.BulletPlayerHitEvent += OnBulletHit;
        GameManager.Instance.GameEvents.RepairingEvent += OnRepairing;

        CurrentHealth = m_playerController.PlayerValues.TotalHealth;
    }

    public override void OnOnDisable()
    {
        GameManager.Instance.GameEvents.BulletPlayerHitEvent -= OnBulletHit;
        GameManager.Instance.GameEvents.RepairingEvent -= OnRepairing;
    }

    private void OnRepairing(string playerName)
    {
        if (playerName != m_playerController.name)
            return;

        m_playerController.StartCoroutine(Repairing());
    }

    private IEnumerator Repairing()
    {
        while (CurrentHealth < 100)
        {
            yield return new WaitForSeconds(3);
            CurrentHealth += 25;
        }
    }

    private void OnBulletHit(string playerName, string enemyName, BulletSO enemyBullet)
    {
        if (playerName == m_playerController.name)
        {
            Debug.Log("selfhurm");
            // return;
        }
        CurrentHealth -= enemyBullet.BulletDamage;
    }
}
