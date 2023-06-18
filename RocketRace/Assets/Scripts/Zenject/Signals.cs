using UnityEngine;

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
