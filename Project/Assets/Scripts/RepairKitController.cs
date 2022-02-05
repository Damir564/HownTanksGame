using UnityEngine;

public class RepairKitController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.gameObject.tag == "Player")
        {

            //collider.transform.parent.gameObject.GetComponent<PlayerController>().StartCoroutine("Repairing");
            GameManager.Instance.GameEvents.RaiseRepairingEvent(collider.transform.parent.name);
            Destroy(transform.gameObject);
            Debug.Log("Player with name \"" + collider.transform.parent.name + "\" зачилился");
        }
    }
}
