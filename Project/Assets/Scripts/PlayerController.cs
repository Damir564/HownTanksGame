using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Carry out do we need a FixedUpdate() or just can use Update()  <-- to-do thing
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed = 1f; //Movement Speed of the Player
    [SerializeField]
    private Vector2 m_movement;         //Movement Axis
    [SerializeField]
    private Rigidbody2D rb;    //Player Rigidbody Component

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_movement.x = Input.GetAxis("Horizontal");
        m_movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + m_movement * m_movementSpeed * Time.fixedDeltaTime);
    }
}
