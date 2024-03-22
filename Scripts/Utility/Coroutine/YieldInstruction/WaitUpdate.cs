using UnityEngine;

public class WaitUpdate : CustomYieldInstruction
{
    private bool _keepWaiting = true;

    public override bool keepWaiting
    {
        get
        {
            _keepWaiting = !_keepWaiting;
            return _keepWaiting;
        }
    }
}