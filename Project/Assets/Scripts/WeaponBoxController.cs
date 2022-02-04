using UnityEngine;

public class WeaponBoxController : MonoBehaviour
{
    [SerializeField] private int m_weaponId;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.gameObject.tag == "Player")
        {
            GameEventSO.Instance.RaiseWeaponChangeEvent(m_weaponId);
            Debug.Log("PlayerInTrigger");
            Destroy(transform.gameObject);
        }
    }
}
