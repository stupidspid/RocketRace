using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using StateMachine;
using UnityEngine;
using Zenject;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private LayerMask loseMask;
    [SerializeField] private LayerMask obstacleMask;
    private SignalBus _signalBus;

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (Utils.IsInLayerMask(other.gameObject, loseMask))
        {
            print("nnn lose");
            _signalBus.Fire(new RequestChangeStateSignal(StateType.Lose));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, obstacleMask))
        {
            _signalBus.Fire(new MovePlayerSignal(false));
        }
    }
}
