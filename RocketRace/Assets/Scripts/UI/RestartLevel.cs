using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class RestartLevel : MonoBehaviour
{
    private Button _restartLevel;
    private SignalBus _signalBus;

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _restartLevel = GetComponent<Button>();
        _restartLevel.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        print("nnn restart");
        _signalBus.Fire(new RequestChangeStateSignal(StateType.LoadLevel));
    }
}
