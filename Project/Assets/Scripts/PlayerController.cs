using UnityEngine;
using UnityEngine.InputSystem;

//Carry out do we need a FixedUpdate() or just can use Update()  <-- to-do thing
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed; //Movement Speed of the Player
    [SerializeField]
    private float m_rotationSpeed;
    [SerializeField]
    private Rigidbody2D rb;    //Player Rigidbody Component
    // [SerializeField]
    // private GameObject LInCircle;
    // private Vector2 m_LInCircleStartPos;

    private PlayerInputActions playerInputAction;
    private bool m_isMovePressed = false;
    private Vector2 m_movement;         //Movement Axis

    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Movement.performed += ctx => MovementEvent(ctx.ReadValue<Vector2>());

        playerInputAction.Player.Movement.canceled += _ => m_isMovePressed = false;
        // PC
        // playerInputAction.Player.Movement2.performed += ctx => MovementEvent(ctx.ReadValue<Vector2>());
        // playerInputAction.Player.Movement2.canceled += _ => m_isMovePressed = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void MovementEvent(Vector2 value)
    {
        m_movement = value;
        m_isMovePressed = true;//value.x != 0 || value.y != 0;
    }

    private void OnEnable()
    {
        playerInputAction.Enable();
    }
    private void OnDisable()
    {
        playerInputAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // // This works with WASD
        // // m_movement.x = Input.GetAxis("Horizontal");
        // // m_movement.y = Input.GetAxis("Vertical");
        // // m_movement = m_movement.normalized;
        // if (Input.GetButton("Fire1"))
        // {
        //     Vector2 apartness = (Vector2)Input.mousePosition - m_LInCircleStartPos;
        //     Debug.Log("apartness: " + apartness);
        //     if (apartness.magnitude <= 50f)
        //     {
        //         m_movement = apartness / 50f;
        //         Debug.Log("if m_movement: " + m_movement);
        //     }
        //     else if (apartness.magnitude <= 300f)
        //     {
        //         m_movement = apartness.normalized;
        //         Debug.Log("else m_movement: " + m_movement);
        //     }
        //     else
        //     {
        //         m_movement = Vector2.zero;
        //     }
        // }
        // if (Input.GetButtonUp("Fire1"))
        // {
        //     m_movement = Vector2.zero;
        // }

        // //For Joystick Controller
        // // if (Input.touchCount > 0){
        // //     //Need to make getting some certain touch later.. Go in foreach maybe only 3-4 touches maximum
        // //     Touch touch = Input.GetTouch(0);
        // //     if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved){
        // //         m_movement = touch.position - m_LInCircleStartPos;
        // //     }
        // //     }
    }

    void FixedUpdate()
    {
        if (m_isMovePressed)
        {
            MovementHandler();
        }
    }

    private void MovementHandler()
    {
        rb.MovePosition(rb.position + m_movement * m_movementSpeed * Time.fixedDeltaTime);
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, m_movement);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.fixedDeltaTime * m_rotationSpeed);

        // float angle = Mathf.Atan2(m_movement.y, m_movement.x) * Mathf.Rad2Deg - 90;
        // Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        // rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, q, Time.deltaTime * m_rotationSpeed);


        //used int, not float. If before or after MovePosition
    }

}
