using UnityEngine;

public class BulletsHit : MonoBehaviour
{
    [SerializeField] private BulletSO m_bulletValues;

    public BulletSO BulletValues
    {
        get => m_bulletValues;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "HitableObject")
        {
            Vector3 soundPos = transform.position;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            Destroy(transform.gameObject, m_bulletValues.BulletSountHitTime);
        }
        else if (collision.transform.tag == "Player")
        {
            Vector3 soundPos = transform.position;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            collision.gameObject.GetComponent<PlayerController>().CurrentHealth -= m_bulletValues.BulletDamage;
            Destroy(transform.gameObject, m_bulletValues.BulletSountHitTime);
        }
    }
}
