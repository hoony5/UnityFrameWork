using System;
using System.Threading.Tasks;
using Share;
using UnityEngine;
using UnityEngine.Networking;
using SuperMaxim;

[Serializable]
public class UnityCoroutine : CustomCoroutine
{
    public event Action OnStart;
    public event Action OnBeforeUpdate;
    public event Action OnAfterUpdate;
    public event Action OnEnd;
    public UnityCoroutine() : base()
    {
               
    }
    public sealed override async void Run()
    {
        _isStop = false;
        Task delay = Task.Delay(delayTime);
        OnStart?.Invoke();
        while (!_isStop)
        {
            if(_activeCoroutines?.Count == 0 || _isPause)
            {
                await delay;
                continue;
            }
            
            for (var index = 0; index < _activeCoroutines?.Count; index++)
            {
                var coroutine = _activeCoroutines[index];
                bool processed = await ProcessCoroutine(coroutine);
                while (!processed)
                {
                    processed = await ProcessCoroutine(coroutine);
                    await Task.Yield();
                }
                await delay;
            }
        }
        OnEnd?.Invoke();
    }

    private async Task<bool> ProcessCoroutine(CustomCoroutineInternal behaviour)
    {
        do
        {
            if (behaviour.routine is null || behaviour.token.Stop || _isStop) break;
            object currentYieldInstruction = behaviour.routine.Current;

            bool result = await TryProcessYieldInstructions(behaviour , currentYieldInstruction);
            if (!result) return false;

            await Task.Delay(delayTime, cancellationTokenSource.Token);
        } while (MoveNext(behaviour, OnBeforeUpdate, OnAfterUpdate));

        _activeCoroutines.SafeRemove(behaviour);
        _activeCoroutinesWithTag.SafeRemove(behaviour.tag);
        return true;
    }

    protected sealed override async Task<bool> TryProcessYieldInstructions(CustomCoroutineInternal behaviour, object currentYieldInstruction)
    {
        if (currentYieldInstruction is null or WaitForFixedUpdate or WaitForEndOfFrame)
        {
            return true;
        }
        else if (currentYieldInstruction is UnityWebRequestAsyncOperation ao)
        {
            if (!ao.isDone)
            {
                return false;
            }
        }
        else if (currentYieldInstruction is ResourceRequest resourceRequest)
        {
            if (!resourceRequest.isDone)
            {
                return false;
            }
        }
        else if (currentYieldInstruction is AsyncOperation asyncOperation)
        {
            if (!asyncOperation.isDone)
            {
                return false;
            }
        }
        else if (currentYieldInstruction is CustomCoroutineToken token)
        {
            if (token.Pause)
            {
                return false;
            }
        }
        else if (currentYieldInstruction is WaitUntil waitUntil)
        {
            if (waitUntil.keepWaiting)
            {
                return false;
            }
        }
        else if (currentYieldInstruction is WaitWhile waitWhile)
        {
            if (waitWhile.keepWaiting)
            {
                return false;
            }
        }
        
        return await base.TryProcessYieldInstructions(behaviour, currentYieldInstruction);
    }

    public override void Dispose()
    {
        base.Dispose();
        OnStart = null;
        OnBeforeUpdate = null;
        OnAfterUpdate = null;
        OnEnd = null;
    }
}