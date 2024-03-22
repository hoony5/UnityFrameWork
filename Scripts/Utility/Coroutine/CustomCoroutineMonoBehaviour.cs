using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class CustomCoroutineMonoBehaviour : MonoBehaviour
{
    public CustomCoroutineSettings settings;
    private CustomCoroutineInternalPool _coroutinePool;
    private CustomCoroutineInternal[] _activeCoroutines;
    private List<int> _availableIndicesList;
    private Queue<int> _freeIndicesQueue;
    private Dictionary<string, List<int>> _usingIndicesDictionary;

    private bool _isStart;
    private bool _isPause;
    private bool _isStop;

    #region Life Cycles
    private void Start()
    {
        Init();
    }

    public void Update()
    {
        if (settings.OptimizeMode) return;
        if (_activeCoroutines.Length == 0) return;

        if (_isStart && !_isPause && !_isStop)
            OnUpdateRoutines();

        if (_isStop)
            RemoveAllRoutines();
    }

    private void FixedUpdate()
    {
        if (!settings.OptimizeMode) return;
        if (_activeCoroutines.Length == 0) return;

        if (_isStart && !_isPause && !_isStop)
            OnUpdateRoutines();

        if (_isStop)
            RemoveAllRoutines();
    }
    #endregion

    #region Initialize & Resize
    public void Init()
    {
        _coroutinePool = new CustomCoroutineInternalPool();
        _usingIndicesDictionary = new Dictionary<string, List<int>>(settings.MaxCoroutines);
        Resize(settings.MaxCoroutines);
    }

    private void InitializeAvailableIndices(int start, int count)
    {
        for (int i = start; i < count; i++)
        {
            _availableIndicesList.Add(i);
            _freeIndicesQueue.Enqueue(i);
        }
    }

    public void Resize(int addRangeLength)
    {
        _availableIndicesList ??= new List<int>(addRangeLength);
        _freeIndicesQueue ??= new Queue<int>(addRangeLength);
        InitializeAvailableIndices(start: _availableIndicesList.Count, count: addRangeLength);

        if(_activeCoroutines is null)
        {
            _activeCoroutines = new CustomCoroutineInternal[settings.MaxCoroutines];
            for (int i = 0; i < _activeCoroutines.Length; i++)
            {
                CustomCoroutineInternal temp = _activeCoroutines[i];
                temp.index = -1;
                _activeCoroutines[i] = temp;
            }
        }
        else
            Array.Resize(ref _activeCoroutines, settings.MaxCoroutines * 2);
    }
    #endregion

    #region Update Routins
    private void OnUpdateRoutines()
    {
        for (int i = 0; i < _activeCoroutines.Length; i++)
        {
            if(_activeCoroutines[i].index != -1)
                ProcessCoroutine(_activeCoroutines[i]);
        }
    }
    private async Task OnUpdateRoutinesAsync()
    {
        await Task.Yield();
        
        for (int i = 0; i < _activeCoroutines.Length; i++)
        {
            if(_activeCoroutines[i].index != -1)
                ProcessCoroutine(_activeCoroutines[i]);
        }
    }
    
    private void RemoveAllRoutines()
    {
        for (int i = _activeCoroutines.Length - 1; i >= 0; i--)
        {
            CustomCoroutineInternal temp = _activeCoroutines[i];
            temp.index = -1;
            _activeCoroutines[i] = temp;
            _coroutinePool.Return(_activeCoroutines[i]);
        }
    }
    #endregion

    #region RequestCreateRepository IEnumerator
    private bool ProcessCoroutine(CustomCoroutineInternal behaviour)
    {
        bool TryProcessYieldInstructions(object currentYieldInstruction)
        {
            if (behaviour.token.Pause)
                return false;

            if (currentYieldInstruction is null or WaitForFixedUpdate or WaitForEndOfFrame)
            {
                behaviour.UpdateWaiting();
                if (behaviour.KeepWaiting())
                    return false;
            }
            else if (currentYieldInstruction is WaitForTime waitForTime)
            {
                if (waitForTime.keepWaiting)
                {
                    return false;
                }

                waitForTime.Reset();
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
            else if (currentYieldInstruction is WaitUpdate waitUpdate)
            {
                if (waitUpdate.keepWaiting)
                {
                    return false;
                }
            }
            else if (currentYieldInstruction is WaitEventTime waitEvent)
            {
                if (waitEvent.keepWaiting)
                {
                    return false;
                }
            }
            else if (currentYieldInstruction is Task task)
            {
                if (!task.IsCompleted)
                {
                    return false;
                }
            }
            else if (currentYieldInstruction is CustomCoroutineInternal nestedCoroutine)
            {
                if (!ProcessNestedCoroutine(nestedCoroutine.routine))
                {
                    return false;
                }
            }
            else if (currentYieldInstruction is IEnumerator nestedRoutine)
            {
                if (!ProcessNestedCoroutine(nestedRoutine))
                {
                    return false;
                }
            }

            return true;
        }

        bool ProcessNestedCoroutine(IEnumerator coroutine)
        {
            do
            {
                if (behaviour.routine is null || behaviour.token.Stop) break;
                object currentYieldInstruction = coroutine.Current;

                if (!TryProcessYieldInstructions(currentYieldInstruction))
                    return false;
            } while (coroutine.MoveNext());
            return true;
        }

        if (settings.DebugMode)
        {
            try
            {
                do
                {
                    if (behaviour.routine is null || behaviour.token.Stop) break;
                    object currentYieldInstruction = behaviour.routine.Current;

                    if (!TryProcessYieldInstructions(currentYieldInstruction))
                        return false;
                    
                } while (behaviour.routine.MoveNext());

                _activeCoroutines[behaviour.index] = new CustomCoroutineInternal(-1, null, new CustomCoroutineToken(-1,false,false,false,false), false);
                _coroutinePool.Return(behaviour);
                _freeIndicesQueue.Enqueue(behaviour.index);
                return true;
            }
            catch (Exception e)
            {
                string tag = "";
                foreach (var kvp in _usingIndicesDictionary)
                {
                    if (kvp.Value.Contains(behaviour.index))
                        tag = kvp.Key;
                }

                if (string.IsNullOrEmpty(tag))
                    Debug.Log($" Index : {behaviour.index} | IsNested : {behaviour.isNested}|"); 
                else
                    Debug.Log($" Tag : {tag} | Index : {behaviour.index} | IsNested : {behaviour.isNested}");
                throw new Exception($"Coroutine failed with exception: {e.Message}");
            }
        }

        do
        {
            if (behaviour.routine is null || behaviour.token.Stop) break;
            object currentYieldInstruction = behaviour.routine.Current;

            if (!TryProcessYieldInstructions(currentYieldInstruction))
                return false;
                
        } while (behaviour.routine.MoveNext());

        _activeCoroutines[behaviour.index] = new CustomCoroutineInternal(-1, null, new CustomCoroutineToken(-1,false,false,false,false), false);
        _coroutinePool.Return(behaviour);
        _freeIndicesQueue.Enqueue(behaviour.index);
        return true;
    }

   public void StartRoutines()
   {
       _isStart = true;
       _isPause = false;
       _isStop = false;
   }
   public void PauseRoutines()
   {
       _isStart = false;
       _isPause = true;
       _isStop = false;
   }
   public void StopRoutines()
   {
       _isStart = false;
       _isPause = false;
       _isStop = true;
   }    

    public CustomCoroutineToken AddRoutine(IEnumerator routine)
    {
        if (_freeIndicesQueue.Count == 0)
        {
            Resize(settings.MaxCoroutines);
        }
        int index = _freeIndicesQueue.Dequeue();
        CustomCoroutineToken token = new CustomCoroutineToken(index, start: true, pause: false, stop: false, syncOrAsync: true);
        CustomCoroutineInternal newBehaviour = _coroutinePool.Get().Init(index, routine, token ,false);
        _activeCoroutines[index] = newBehaviour;

        return token;
    }
    public CustomCoroutineToken AddRoutineWithTag(string tag, IEnumerator routine)
    {
        if (_freeIndicesQueue.Count == 0)
        {
            Resize(settings.MaxCoroutines);
        }

        int index = _freeIndicesQueue.Dequeue();

        // Update the dictionary with the new index
        if (_usingIndicesDictionary.TryGetValue(tag, out List<int> indicesList))
        {
            indicesList.Add(index);
        }
        else
        {
            _usingIndicesDictionary[tag] = new List<int> { index };
        }

        CustomCoroutineToken token = new CustomCoroutineToken(index, start: true, pause: false, stop: false, syncOrAsync: true);
        CustomCoroutineInternal newBehaviour = _coroutinePool.Get().Init(index, routine, token, false);
        _activeCoroutines[index] = newBehaviour;

        return token;
    }
    #endregion

    #region Token Methods
    public void ResumeRoutine(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Length <= token.Index) return;
        _activeCoroutines[token.Index].OnStart();
    }

    public void PauseRoutine(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Length <= token.Index) return;
        _activeCoroutines[token.Index].OnPause();
    }

    public void ConvertToAsync(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Length <= token.Index) return;
        _activeCoroutines[token.Index].OnAsync();
    }

    public void ConvertToSync(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Length <= token.Index) return;
        _activeCoroutines[token.Index].OnSync();
    }
    public void ResumeRoutine(CustomCoroutineToken[] tokens)
    {
        foreach (CustomCoroutineToken token in tokens)
        {
            if (_activeCoroutines.Length <= token.Index) return;
            _activeCoroutines[token.Index].OnStart();   
        }
    }

    public void PauseRoutines(CustomCoroutineToken[] tokens)
    {
        foreach (CustomCoroutineToken token in tokens)
        {
            if (_activeCoroutines.Length <= token.Index) return;
            _activeCoroutines[token.Index].OnPause();
        }
    }

    public void ConvertToAsyncs(CustomCoroutineToken[] tokens)
    {
        foreach (CustomCoroutineToken token in tokens)
        {
            if (_activeCoroutines.Length <= token.Index) return;
            _activeCoroutines[token.Index].OnAsync();
        }
    }

    public void ConvertToSyncs(CustomCoroutineToken[] tokens)
    {
        foreach (CustomCoroutineToken token in tokens)
        {
            if (_activeCoroutines.Length <= token.Index) return;
            _activeCoroutines[token.Index].OnSync();
        }
    }
    #endregion

    #region Tracking Queries

    public void DebugActiveCoroutines()
    {
        for (var index = 0; index < _activeCoroutines?.Length; index++)
        {
            var c = _activeCoroutines[index];
            Debug.Log($"{index} | {c.index} | {c.routine is null}");
        }
    }
    public bool AnyExist(string tag)
    {
        return _usingIndicesDictionary.ContainsKey(tag) && _usingIndicesDictionary[tag].Count > 0;
    }

    public bool HasRoutine(string tag, CustomCoroutineToken token)
    {
        if (_usingIndicesDictionary.TryGetValue(tag, out var indicesList))
        {
            return indicesList.Contains(token.Index);
        }
        return false;
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
}