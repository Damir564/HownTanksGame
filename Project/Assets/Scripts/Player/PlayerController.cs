using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerModule[] playerModules;

    // Movement & Rotation
    [SerializeField] private PlayerSO m_playerValues;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private GameObject m_body;
    [SerializeField] private GameObject m_headHandler;

    // Input
    private Vector2 m_movement;
    private Vector2 m_aiming;
    private bool m_isMovePressed = false;
    private bool m_isAimingPressed = false;

    //Animations
    [SerializeField] private GameObject m_repairingAnimationObject;

    public GameObject HeadHandler
    {
        get => m_headHandler;
    }

    // public GameObject RepairingAnimationObject
    // {
    //     get => m_repairingAnimationObject;
    // }

    public PlayerSO PlayerValues
    {
        get => m_playerValues;
    }

    private void Awake()
    {

        foreach (PlayerModule module in playerModules)
            module.OnAwake();
    }


    private void OnEnable()
    {

        // Input
        GameManager.Instance.InputEvents.MovementPerformedEvent += MovementPerformedEvent;
        GameManager.Instance.InputEvents.MovementCanceledEvent += MovementCanceledEvent;
        GameManager.Instance.InputEvents.AimingPerformedEvent += AimingPerformedEvent;
        GameManager.Instance.InputEvents.AimingCanceledEvent += AimingCanceledEvent;

        foreach (PlayerModule module in playerModules)
            module.OnOnEnable();
    }
    private void OnDisable()
    {
        // Input
        GameManager.Instance.InputEvents.MovementPerformedEvent -= MovementPerformedEvent;
        GameManager.Instance.InputEvents.MovementCanceledEvent -= MovementCanceledEvent;
        GameManager.Instance.InputEvents.AimingPerformedEvent -= AimingPerformedEvent;
        GameManager.Instance.InputEvents.AimingCanceledEvent -= AimingCanceledEvent;

        foreach (PlayerModule module in playerModules)
            module.OnOnDisable();
    }

    //Input
    private void MovementPerformedEvent(Vector2 value)
    {
        m_movement = value;
        m_isMovePressed = true;
    }

    private void MovementCanceledEvent()
    {
        m_isMovePressed = false;
    }

    private void AimingPerformedEvent(Vector2 value)
    {
        m_aiming = value;
        m_isAimingPressed = true;
    }

    private void AimingCanceledEvent()
    {
        m_isAimingPressed = false;
    }

    // Movement & Rotation
    void FixedUpdate()
    {
        if (m_isMovePressed)
            MovementHandler();
        if (m_isAimingPressed)
            AimingHandler();
    }

    private void MovementHandler()
    {
        m_rb.MovePosition(m_rb.position + m_movement * m_playerValues.MovementSpeed * Time.fixedDeltaTime);
        Quaternion toBodyRotation = Quaternion.LookRotation(Vector3.forward, m_movement);
        m_body.transform.rotation = Quaternion.RotateTowards(m_body.transform.rotation, toBodyRotation, Time.fixedDeltaTime * m_playerValues.RotationSpeedBody);
    }

    private void AimingHandler()
    {
        Quaternion toHeadRotation = Quaternion.LookRotation(Vector3.forward, m_aiming);
        m_headHandler.transform.rotation = Quaternion.RotateTowards(m_headHandler.transform.rotation, toHeadRotation, Time.fixedDeltaTime * m_playerValues.RotationSpeedHead);
        if (m_aiming.sqrMagnitude >= m_playerValues.ShootZone)
            GameManager.Instance.PlayerEvents.RaiseShootingEvent();
    }

    public void AnimationChange(bool b)
    {
        m_repairingAnimationObject.SetActive(b);
    }
}
