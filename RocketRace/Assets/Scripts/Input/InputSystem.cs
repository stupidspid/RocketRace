using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputSystem : MonoBehaviour
{
    private SignalBus _signalBus;
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;
    private Vector2 fingerCurrentPos;

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void Update()
    {
        _signalBus.Fire(new InputFingerSignal(Input.touchCount > 0));
        if (Input.touchCount < 0) return;
        foreach (Touch touch in Input.touches) 
        {
            if (touch.phase == TouchPhase.Began) 
            {
                fingerDownPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved) 
            {
                fingerCurrentPos = touch.position;
                DetectSwipe ();
            }
            else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                fingerUpPos = touch.position;
                _signalBus.Fire(new InputMoveSignal(0));
            }
        }
    }
    
    void DetectSwipe ()
    {
        var fingerPositionDiff = (fingerCurrentPos.x - fingerDownPos.x);
        _signalBus.Fire(new InputMoveSignal(-fingerPositionDiff));
    }
}
