using UnityEngine;

public class ObjectsHit : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_simpleBulletClip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if will be needed: audioclip by bullet tag
        if (collision.gameObject.tag == "Projectile")
        {
            Vector3 soundPos = collision.transform.position;
            collision.gameObject.SetActive(false);
            Transform bulletParent = collision.transform.parent;
            collision.transform.parent.GetChild(1).gameObject.SetActive(true);
            Destroy(bulletParent.gameObject, 0.2f);
        }
    }
}
