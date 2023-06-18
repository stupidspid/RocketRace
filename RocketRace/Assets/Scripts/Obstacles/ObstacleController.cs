using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void Start()
    {
        _obstacleTransform = transform;
    }

    private void Move()
    {
        _obstacleTransform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
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

