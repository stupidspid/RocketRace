using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;
using Zenject;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float forwardMoveSpeed;
    [SerializeField] private float horizontalMoveSpeed;
    private SignalBus _signalBus;
    private float _horizontalMoveDirection;
    private bool _isMoving;
    private bool _isAbleToMove;
    private bool _isLose;

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<InputMoveSignal>(GetMoveState);
        _signalBus.Subscribe<InputFingerSignal>(IsMove);
        _signalBus.Subscribe<GameStateChangedSignal>(GetCurrentGameState);
        _signalBus.Subscribe<MovePlayerSignal>(GetObstacleCollision);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<InputMoveSignal>(GetMoveState);
        _signalBus.Unsubscribe<InputFingerSignal>(IsMove);
    }

    private void GetCurrentGameState(GameStateChangedSignal signal)
    {
        if (signal.currentGameStateType == StateType.InGame)
        {
            _isAbleToMove = true;
            rigidbody.isKinematic = false;
        }
        
        if (signal.currentGameStateType == StateType.LoadLevel ||
            signal.currentGameStateType == StateType.MainMenu)
        {
            _isAbleToMove = false;
            _isMoving = false;
            rigidbody.isKinematic = true;
        }
    }

    private void GetMoveState(InputMoveSignal signal)
    {
        _horizontalMoveDirection = signal.horizontalInput;
    }

    private void IsMove(InputFingerSignal signal)
    {
        _isMoving = signal.isHolding;
    }

    private void Move()
    {
        if(!_isMoving) return;
        if(_isLose) return;
        var relativeUpSpeed = forwardMoveSpeed * Vector3.up;
        var relativeHorizontalSpeed = horizontalMoveSpeed * _horizontalMoveDirection * Vector3.right;
        var currentRelativeSpeed = relativeUpSpeed + relativeHorizontalSpeed;
        rigidbody.velocity = currentRelativeSpeed * Time.deltaTime;
    }

    private void Update()
    {
        if (_isLose) return;
        Move();
        UpdateGravity();
    }

    private void UpdateGravity()
    {
        rigidbody.useGravity = !_isMoving && _isAbleToMove;
    }

    private void GetObstacleCollision(MovePlayerSignal signal)
    {
        _isLose = !signal.isAvailableMove;
        rigidbody.useGravity = true;
    }
}
