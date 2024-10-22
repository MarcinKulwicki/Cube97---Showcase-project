using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance
    {
        get
        {
            if (_component == null)
                Debug.LogError("There is no instance of object");

            return _component;
        }
    }

    private static T _component;

    [SerializeField]
    private bool _dontDestroyOnLoad;

    protected virtual void Awake()
    {
        _component = GetComponent<T>();

        if (_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
