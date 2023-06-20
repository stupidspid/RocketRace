using StateMachine;
using UnityEngine;

public class GameStateChangedSignal
{
    public StateType currentGameStateType;
    public int levelNum;

    public GameStateChangedSignal(StateType stateType)
    {
        currentGameStateType = stateType;
    }
    public GameStateChangedSignal(StateType stateType, int levelNum)
    {
        currentGameStateType = stateType;
        this.levelNum = levelNum;
    }
}

public class RequestChangeStateSignal
{
    public StateType newStateType;
    public float delay = 1;
    public RequestChangeStateSignal(StateType stateType, float delay = 0.5f)
    {
        newStateType = stateType;
        this.delay = delay;
    }
        
    public RequestChangeStateSignal(StateType stateType)
    {
        newStateType = stateType;
    }
}

public class InputMoveSignal
{
    public float horizontalInput;

    public InputMoveSignal(float horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }
}

public class InputFingerSignal
{
    public bool isHolding;
        
    public InputFingerSignal(bool isHolding)
    {
        this.isHolding = isHolding;
    }
}

public class UpdateObstacleSignal
{
    public Transform transform;

    public UpdateObstacleSignal(Transform transform)
    {
        this.transform = transform;
    }
}

public class MovePlayerSignal
{
    public bool isAvailableMove;

    public MovePlayerSignal(bool isAvailableMove)
    {
        this.isAvailableMove = isAvailableMove;
    }
}
