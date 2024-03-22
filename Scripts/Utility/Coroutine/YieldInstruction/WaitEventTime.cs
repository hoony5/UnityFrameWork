using System;
using UnityEngine;

public class WaitEventTime : CustomYieldInstruction
{
    private Func<bool> _predicate;
    private bool _wake;
    private bool _stay;
    private bool _useUntil;
    private bool _useWhile;

    
    public WaitEventTime()
    {
    }

    public WaitEventTime(Func<bool> predicate)
    {
        _predicate = predicate;
    }

    // if you used this it is no alloc, when using predicate which not captured. Try using static instead of captured or lamda.
    public WaitEventTime WaitWhile(Func<bool> predicate)
    {
        UseWaitWhileType();
        _predicate = predicate;
        return this;
    }

    // if you used this it is no alloc, when using predicate which not captured. Try using static instead of captured or lamda.
    public WaitEventTime WaitUntil(Func<bool> predicate)
    {
        UseWaitUntilType();
        _predicate = predicate;
        return this;
    }

    public void Dispose()
    {
        _predicate = null;
    }

    // reset flags
    private void UseWaitWhileType()
    {
        _useUntil = false;
        _useWhile = true;
    }

    // reset flags
    private void UseWaitUntilType()
    {
        _useUntil = true;
        _useWhile = false;
    }

    public override bool keepWaiting
    {
        get
        {
            // until is same WaitUntil and while is same WaitWhile.
            _wake = _useUntil ? _predicate.Invoke() : _useWhile ? !_predicate.Invoke() : !_wake;

            if (_wake)
            {
                _wake = false;
                    
                if(!_stay) 
                    return _stay = true;
                    
                return _stay = false;
            }

            return _stay = true;
        }
    }
}