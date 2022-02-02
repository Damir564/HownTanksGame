using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                T[] assets = Resources.LoadAll<T>("");
                if (assets == null || assets.Length < 1)
                    Debug.LogError("There is no Singleton ScriptableObject base class");
                else if (assets.Length > 1)
                    Debug.LogError("There are a lot of Singleton ScriptableObject base classes");
                m_instance = assets[0];
            }
            return m_instance;
        }
    }
}
