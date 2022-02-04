using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxController : MonoBehaviour
{
    [SerializeField] private int m_ammoAmount;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.gameObject.tag == "Player")
        {
            Debug.Log("PlayerInTrigger");
            // collider.transform.parent.gameObject.GetComponent<PlayerController>().CurrentAllAmmo = 1;
            Destroy(transform.gameObject);
        }
    }
}
