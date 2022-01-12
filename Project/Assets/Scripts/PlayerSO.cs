using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSO", order = 1)]
public class PlayerSO : ScriptableObject
{
    public ScriptableObject[] m_bulletSOs = new ScriptableObject[5];
}
