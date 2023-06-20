using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using StateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask finishMask;
    private Transform _obstacleTransform;
    private SignalBus _signalBus;
    private bool _isAbleToMove;

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<GameStateChangedSignal>(GetChangedGameState);
    }

    private void GetChangedGameState(GameStateChangedSignal signal)
    {
        if (signal.currentGameStateType != StateType.InGame)
        {
            _isAbleToMove = false;
        }
        else
        {
            _isAbleToMove = true;
        }
    }
    private void Start()
    {
        _obstacleTransform = transform;
    }

    private void Move()
    {
        _obstacleTransform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(!_isAbleToMove) return;
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Utils.IsInLayerMask(other.gameObject, finishMask))
        {
            _signalBus.Fire(new UpdateObstacleSignal(transform));
        }
    }
}

