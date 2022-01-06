using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Carry out do we need a FixedUpdate() or just can use Update()  <-- to-do thing
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed = 1f; //Movement Speed of the Player
    public float MovementSpeed
    {
        get => m_movementSpeed;
        set => m_movementSpeed = value;
    }
    [SerializeField]
    private Vector2 m_movementVector; //Movement Axis
    public Vector2 MovementVector
    {
        get => m_movementVector;
        set => m_movementVector = value;
    }
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
        m_movementVector.x = Input.GetAxis("Horizontal");
        m_movementVector.y = Input.GetAxis("Vertical");
        m_movementVector = m_movementVector.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + m_movementVector * m_movementSpeed * Time.fixedDeltaTime);
    }
}
