using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationMultiplier;
    private float _horizontalRotateDirection;
    private bool _isMoving;
    private SignalBus _signalBus;

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<InputMoveSignal>(GetRotateState);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<InputMoveSignal>(GetRotateState);
    }

    private void GetRotateState(InputMoveSignal signal)
    {
        var rotationValue = signal.horizontalInput * rotationMultiplier;
        _horizontalRotateDirection = (signal.horizontalInput != 0) ? rotationValue : 0;
    }

    private void Rotate()
    {
        var rotation = Quaternion.ToEulerAngles(playerRigidbody.rotation);
        rotation.z += _horizontalRotateDirection;
        playerRigidbody.AddTorque(rotation * rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Rotate();
    }
}
