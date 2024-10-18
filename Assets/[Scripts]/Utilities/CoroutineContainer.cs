using System.Collections;
using UnityEngine;

public class CoroutineContainer : MonoBehaviour
{
    private Coroutine _ref;

    /// <summary>
    ///     Creating new container for Coroutine, new container is created in DontDestroyOnLoad and removed at the end or on Interrupt.
    /// </summary>
    /// <param name="coroutine"></param>
    /// <returns></returns>
    public static CoroutineContainer Create(IEnumerator coroutine)
    {
        CoroutineContainer container = new GameObject("CoroutineContainer").AddComponent<CoroutineContainer>();
        DontDestroyOnLoad(container.gameObject);
        container._ref = container.StartCoroutine(container.RoutineWrapper(coroutine));
        return container;
    }

    public void Interrupt()
    {
        if (_ref != null)
        {
            StopCoroutine(_ref);
            _ref = null;
            Destroy(gameObject);
        }
    }

    private IEnumerator RoutineWrapper(IEnumerator coroutine)
    {
        yield return coroutine;
        Destroy(gameObject);
    }
}