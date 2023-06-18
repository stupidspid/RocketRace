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
