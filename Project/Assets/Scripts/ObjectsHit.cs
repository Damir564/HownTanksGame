using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsHit : MonoBehaviour
{
    // private Transform soundPos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            // soundPos = collision.transform;
            // Debug.Log("soundPos = " + soundPos);
            Destroy(collision.gameObject);
        }
    }
}
