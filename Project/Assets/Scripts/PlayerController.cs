using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Carry out do we need a FixedUpdate() or just can use Update()  <-- to-do thing
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f; //Movement Speed of the Player
    public Vector2 movement;         //Movement Axis
    public Rigidbody2D rb;    //Player Rigidbody Component

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
