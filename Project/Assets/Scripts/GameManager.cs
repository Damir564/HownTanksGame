using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEventsSO m_gameEvents;
    [SerializeField] private InputEventsSO m_inputEvents;
    [SerializeField] private PlayerEventsSO m_playerEvents;
    [SerializeField] private ModeSO m_mode;
    [SerializeField] private MapSO m_map;

    public GameEventsSO GameEvents
    {
        get => m_gameEvents;
    }
    public InputEventsSO InputEvents
    {
        get => m_inputEvents;
    }
    public PlayerEventsSO PlayerEvents
    {
        get => m_playerEvents;
    }
    public ModeSO Mode
    {
        get => m_mode;
    }
    public MapSO Map
    {
        get => m_map;
    }

    private static GameManager m_instance;

    public static GameManager Instance
    {
        get => m_instance;
    }

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else
            Destroy(this);
    }
}
