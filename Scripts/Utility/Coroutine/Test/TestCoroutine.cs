using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    UnityCoroutine cc = new UnityCoroutine();
    IEnumerator Routine()
    {
        WaitUpdate waitUpdate = new WaitUpdate();
        int count = 0;
        while (count < 100)
        {
            count++;
            Debug.Log("Routine A");
            yield return RoutineB();
        }
        
        Debug.Log("Routine A End");
    }
    
    IEnumerator RoutineB()
    {
        WaitUpdate waitUpdate = new WaitUpdate();
        int count = 0;
        while (count < 100)
        {
            count++;
            Debug.Log("Routine B");
            yield return waitUpdate;
        }
        
        Debug.Log("Routine B End");
    }

    public void RegisterEvents()
    {
        cc.OnStart += () => Debug.Log("OnStart");
        cc.OnBeforeUpdate += () => Debug.Log("OnBeforeUpdate");
        cc.OnAfterUpdate += () => Debug.Log("OnAfterUpdate");
        cc.OnEnd += () => Debug.Log("OnEnd");
    }
    [Button]
    public void SetUp()
    {
        cc.AddRoutine(Routine());
        RegisterEvents();
    }
    [Button]
    public void Run()
    {
        cc.Run();
    }
    [Button]
    public void Break()
    {
        cc.Break();
    }
}
