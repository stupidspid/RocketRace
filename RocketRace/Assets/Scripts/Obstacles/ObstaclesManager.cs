using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private int minObstaclesAmount;
    [SerializeField] private int maxObstaclesAmount;
    [SerializeField] private int sidesAmount = 2;
    [SerializeField] private float respawnYPosition;
    [SerializeField] private float respawnXPosition;
    private SignalBus _signalBus;
    private int _obstaclesCounter;
    private int _currentFrequency;
    private float _randomXPosition;
    private int _randomSide;
    private List<Transform> _obstacles = new List<Transform>();

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<UpdateObstacleSignal>(CountObstacles);
    }

    private void OnEnable()
    {
        RandomizeFrequency();
    }

    private void RandomizeFrequency()
    {
        _currentFrequency = Random.Range(minObstaclesAmount, maxObstaclesAmount);
        _randomXPosition = Random.Range(0, respawnXPosition);
        _randomSide = Random.Range(0, sidesAmount);
    }

    private void CountObstacles(UpdateObstacleSignal signal)
    {
        _obstaclesCounter++;
        if (Mathf.FloorToInt(_obstaclesCounter / sidesAmount) == _currentFrequency)
        {
            if (_obstacles.Count < sidesAmount)
            {
                _obstacles.Add(signal.transform);
            }
        }
        SetObstaclePosition(signal.transform);
        ClearRandomize();
    }

    private void SetObstaclePosition(Transform obstacleTransform)
    {
        var newXPosition = _obstacles.Count == sidesAmount ? _randomXPosition : 0;
        obstacleTransform.localPosition = new Vector3(newXPosition, respawnYPosition, 0);
    }


    private void ClearRandomize()
    {
        if (_obstacles.Count != sidesAmount) return;
        _obstacles.Clear();
        RandomizeFrequency();
        _obstaclesCounter = 0;
    }
}
