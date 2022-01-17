using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsHit : MonoBehaviour
{
    private Vector3 soundPos;
    [SerializeField]
    private AudioClip m_simpleBulletClip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if will be needed: audioclip by bullet tag
        if (collision.gameObject.tag == "Projectile")
        {
            soundPos = collision.transform.position;
            Debug.Log(soundPos);
            AudioSource.PlayClipAtPoint(m_simpleBulletClip, soundPos, 0.5f);
            // m_audioSource.transform.position = soundPos;
            // m_audioSource.PlayOneShot(m_simpleBulletClip);
            Destroy(collision.gameObject);
        }
    }
}
