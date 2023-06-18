using System;
using System.Collections;
using System.Collections.Generic;
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

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<InputMoveSignal>(GetMoveState);
        _signalBus.Subscribe<InputFingerSignal>(IsMove);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<InputMoveSignal>(GetMoveState);
        _signalBus.Unsubscribe<InputFingerSignal>(IsMove);
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
        var relativeUpSpeed = forwardMoveSpeed * Vector3.up;
        var relativeHorizontalSpeed = horizontalMoveSpeed * _horizontalMoveDirection * Vector3.right;
        var currentRelativeSpeed = relativeUpSpeed + relativeHorizontalSpeed;
        rigidbody.velocity = currentRelativeSpeed * Time.deltaTime;
    }

    private void Update()
    {
        Move();
        UpdateGravity();
    }

    private void UpdateGravity()
    {
        rigidbody.useGravity = !_isMoving;
    }

}
