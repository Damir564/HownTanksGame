using UnityEngine;


public abstract class PlayerModule : MonoBehaviour, IModule
{
    [SerializeField] protected PlayerController m_playerController;

    public virtual void OnAwake() { }
    public virtual void OnOnDisable() { }
    public virtual void OnOnEnable() { }
}
