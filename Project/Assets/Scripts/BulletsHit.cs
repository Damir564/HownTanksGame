using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsHit : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "HitableObject")
        {
            Destroy(gameObject);
        }
    }
}
