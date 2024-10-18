using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerLayer : MonoBehaviour
{
    public static Action Reset;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private UnityEvent<Collider> _onEnter, _onExit;
    private bool _isInTrigger;
    private int _mask;

    private void Awake() => _mask = _layerMask.value;

    private void OnEnable() => Reset += OnReset;

    private void OnDisable() => Reset -= OnReset;

    private void OnReset() => _isInTrigger = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (_isInTrigger)
            return;
        if ((_mask & (1 << other.gameObject.layer)) > 0)
        {
            _onEnter?.Invoke(other);
            _isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (!_isInTrigger)
            return;
        if ((_mask & (1 << other.gameObject.layer)) > 0)
        {
            _onExit?.Invoke(other);
            _isInTrigger = false;
        }
    }
}
