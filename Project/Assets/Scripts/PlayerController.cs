using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Carry out do we need a FixedUpdate() or just can use Update()  <-- to-do thing
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed = 0.01f; //Movement Speed of the Player
    [SerializeField]
    private Vector2 m_movement;         //Movement Axis
    [SerializeField]
    private Rigidbody2D rb;    //Player Rigidbody Component
    [SerializeField]
    private GameObject LInCircle;
    private Vector2 m_LInCircleStartPos; 


    //For Joystick Controller


    // Start is called before the first frame update
    void Start()
    {
        m_LInCircleStartPos = LInCircle.transform.position;  //Vector 2 = Vector 3 Will it work? - Yes.
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // This works with WASD
        // m_movement.x = Input.GetAxis("Horizontal");
        // m_movement.y = Input.GetAxis("Vertical");
        // m_movement = m_movement.normalized;
        if (Input.GetButton("Fire1")){
            Vector2 apartness = (Vector2) Input.mousePosition - m_LInCircleStartPos;
            Debug.Log("apartness: " + apartness);
            if (apartness.magnitude <= 50f){
                m_movement = apartness / 50f;
                Debug.Log("if m_movement: " + m_movement);
            }
            else if(apartness.magnitude <= 300f){
                m_movement = apartness.normalized;
                Debug.Log("else m_movement: " + m_movement);
            }
            else{
                m_movement = Vector2.zero;
            }
        }
        if (Input.GetButtonUp("Fire1")){
             m_movement = Vector2.zero;
        }

        //For Joystick Controller
        // if (Input.touchCount > 0){
        //     //Need to make getting some certain touch later.. Go in foreach maybe only 3-4 touches maximum
        //     Touch touch = Input.GetTouch(0);
        //     if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved){
        //         m_movement = touch.position - m_LInCircleStartPos;
        //     }
        //     }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + m_movement * m_movementSpeed * Time.fixedDeltaTime);

        if (m_movement != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, m_movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 9999 * Time.fixedDeltaTime);  //used int, not float. If before or after MovePosition
        }
    }
}
