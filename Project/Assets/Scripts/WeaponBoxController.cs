using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoxController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.gameObject.tag == "Player")
        {
            Debug.Log("PlayerInTrigger");
            collider.transform.parent.gameObject.GetComponent<PlayerController>().weaponChange(4);
            Destroy(transform.gameObject);
        }
    }
}
