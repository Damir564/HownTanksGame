using UnityEngine;
using UnityEngine.Events;

public class BulletsHit : MonoBehaviour
{
    [SerializeField] private BulletSO m_bulletValues;

    public BulletSO BulletValues
    {
        get => m_bulletValues;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.CompareTag("HitableObject"))
        {
            Vector3 soundPos = transform.position;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            Destroy(transform.gameObject, m_bulletValues.BulletSountHitTime);
        }
        else if (collision.transform.CompareTag("Player"))
        {
            Vector3 soundPos = transform.position;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            GameEventSO.Instance.RaiseBulletPlayerHitEvent(collision.gameObject.name, gameObject.name, m_bulletValues);
            Debug.Log("Player with name \"" + collision.gameObject.name + "\" получил пулю");
            Destroy(transform.gameObject, m_bulletValues.BulletSountHitTime);
        }
    }
}
