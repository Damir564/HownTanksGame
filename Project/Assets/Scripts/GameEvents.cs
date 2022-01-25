using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event System.Action onEnvObjEnter;
    public void EnvObjEnter()
    {
        if (onEnvObjEnter != null)
        {
            onEnvObjEnter();
        }
    }

}
