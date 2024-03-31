using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Share;
using UnityEngine;

[Serializable]
public class CustomCoroutine : IDisposable
{
    protected List<CustomCoroutineInternal> _activeCoroutines;
    protected Dictionary<string, List<CustomCoroutineInternal>> _activeCoroutinesWithTag;
    protected CancellationTokenSource cancellationTokenSource;
    protected readonly int delayTime = 16;
    protected bool _isPause;
    protected bool _isStop;
    
    public event Action OnStart;
    public event Action OnBeforeUpdate;
    public event Action OnAfterUpdate;
    public event Action OnEnd;
    
    #region Life Cycles
    public CustomCoroutine()
    {
        cancellationTokenSource = new CancellationTokenSource();
        _activeCoroutines = new List<CustomCoroutineInternal>();
        _activeCoroutinesWithTag = new Dictionary<string, List<CustomCoroutineInternal>>();
    }

    public virtual async void Run()
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
                CustomCoroutineInternal coroutine = _activeCoroutines[index];
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

    public void Break()
    {
        _isStop = true;
    }
    #endregion

    #region Update Routins
    private void RemoveAllRoutines()
    {
        _activeCoroutines.Clear();
    }
    #endregion

    #region RequestCreateRepository IEnumerator

    protected bool MoveNext(CustomCoroutineInternal behaviour, Action onBeforeUpdate, Action onAfterUpdate)
    {
        onBeforeUpdate?.Invoke();
        bool next = behaviour.routine.MoveNext();
        onAfterUpdate?.Invoke();
        return next;
    }
    private async Task<bool> ProcessCoroutine(CustomCoroutineInternal behaviour)
    {
        do
        {
            if (behaviour.routine is null || behaviour.token.Stop || _isStop) break;
            
            object currentYieldInstruction = behaviour.routine.Current;
        
            bool result = await TryProcessYieldInstructions(behaviour, currentYieldInstruction);
            if (!result) return false;
            await Task.Delay(delayTime, cancellationTokenSource.Token);
        } while (MoveNext(behaviour, OnBeforeUpdate, OnAfterUpdate));

        _activeCoroutines.SafeRemove(behaviour);
        _activeCoroutinesWithTag.SafeRemove(behaviour.tag);
        return true;
    }

    protected virtual async Task<bool> ProcessNestedCoroutine(CustomCoroutineInternal behaviour, IEnumerator coroutine)
    {
        do
        {
            if (behaviour.routine is null || behaviour.token.Stop) break;
            object currentYieldInstruction = coroutine.Current;

            bool result = await TryProcessYieldInstructions(behaviour, currentYieldInstruction);
            if (!result) return false;

            await Task.Delay(delayTime, cancellationTokenSource.Token);
        } while (coroutine.MoveNext());

        return true;
    }

    protected virtual async Task<bool> TryProcessYieldInstructions(CustomCoroutineInternal behaviour,
        object currentYieldInstruction)
    {
        if (behaviour.token.Pause)
            return false;
        
        if (currentYieldInstruction is CustomYieldInstruction yieldInstruction)
        {
            if (yieldInstruction.keepWaiting)
            {
                return false;
            }

            yieldInstruction.Reset();
        }
        else if (currentYieldInstruction is Task task)
        {
            if (!task.IsCompleted)
            {
                return false;
            }
        }
        else if (currentYieldInstruction is ValueTask vTask)
        {
            if (!vTask.IsCompleted)
            {
                return false;
            }
        }
        else if (currentYieldInstruction is CustomCoroutineInternal nestedCoroutine)
        {
            bool nestedResult = await ProcessNestedCoroutine(behaviour, nestedCoroutine.routine);
            if (!nestedResult)
            {
                return false;
            }
        }
        else if (currentYieldInstruction is IEnumerator nestedRoutine)
        {
            bool nestedResult = await ProcessNestedCoroutine(behaviour, nestedRoutine);
            if (!nestedResult)
            {
                return false;
            }
        }

        return true;
    }

    public void StartRoutines()
   {
       _isPause = false;
       _isStop = false;
   }
   public void PauseRoutines()
   {
       _isPause = true;
       _isStop = false;
   }
   public void StopRoutines()
   {
       _isPause = false;
       _isStop = true;
   }    

    public CustomCoroutineToken AddRoutine(IEnumerator routine) => AddRoutineWithTag("default", routine);
    
    public CustomCoroutineToken AddRoutineWithTag(string tag, IEnumerator routine)
    {
        CustomCoroutineToken token = new CustomCoroutineToken(_activeCoroutines.Count, start: true, pause: false, stop: false, syncOrAsync: true);
        CustomCoroutineInternal newBehaviour = new CustomCoroutineInternal(_activeCoroutines.Count, tag, routine, token);
        _activeCoroutines.Add(newBehaviour);
        _activeCoroutinesWithTag.AddOrUpdate(tag, newBehaviour);
        return token;
    }
    #endregion

    #region Token Methods
    public void ResumeRoutine(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Count <= token.Index) return;
        _activeCoroutines[token.Index].OnStart();
    }

    public void PauseRoutine(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Count <= token.Index) return;
        _activeCoroutines[token.Index].OnPause();
    }

    public void ConvertToAsync(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Count <= token.Index) return;
        _activeCoroutines[token.Index].OnAsync();
    }

    public void ConvertToSync(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Count <= token.Index) return;
        _activeCoroutines[token.Index].OnSync();
    }
    public void ResumeRoutine(CustomCoroutineToken[] tokens)
    {
        foreach (CustomCoroutineToken token in tokens)
        {
            if (_activeCoroutines.Count <= token.Index) return;
            _activeCoroutines[token.Index].OnStart();   
        }
    }

    public void PauseRoutines(CustomCoroutineToken[] tokens)
    {
        foreach (CustomCoroutineToken token in tokens)
        {
            if (_activeCoroutines.Count <= token.Index) return;
            _activeCoroutines[token.Index].OnPause();
        }
    }

    public void ConvertToAsyncs(CustomCoroutineToken[] tokens)
    {
        foreach (CustomCoroutineToken token in tokens)
        {
            if (_activeCoroutines.Count <= token.Index) return;
            _activeCoroutines[token.Index].OnAsync();
        }
    }

    public void ConvertToSyncs(CustomCoroutineToken[] tokens)
    {
        foreach (CustomCoroutineToken token in tokens)
        {
            if (_activeCoroutines.Count <= token.Index) return;
            _activeCoroutines[token.Index].OnSync();
        }
    }
    #endregion

    #region Tracking Queries

    public void DebugActiveCoroutines()
    {
        for (var index = 0; index < _activeCoroutines?.Count; index++)
        {
            var c = _activeCoroutines[index];
            Debug.Log($"{index} | {c.index} | {c.routine is null}");
        }
    }
    public bool AnyExist(string tag)
    {
        return _activeCoroutinesWithTag.ContainsKey(tag) && _activeCoroutinesWithTag[tag].Count > 0;
    }
    public bool HasRoutine(CustomCoroutineToken token)
    {
        foreach (CustomCoroutineInternal activeCoroutine in _activeCoroutines)
        {
            if (activeCoroutine.index == token.Index) return true;
        }

        return false;
    }
    public bool HasRoutine(int index)
    {
        foreach (CustomCoroutineInternal activeCoroutine in _activeCoroutines)
        {
            if (activeCoroutine.index == index) return true;
        }

        return false;
    }
    #endregion

    public virtual void Dispose()
    {
        cancellationTokenSource?.Dispose();
        OnStart = null;
        OnBeforeUpdate = null;
        OnAfterUpdate = null;
        OnEnd = null;
    }
}