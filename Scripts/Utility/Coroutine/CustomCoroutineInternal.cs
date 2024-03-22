using System.Collections;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct CustomCoroutineInternal
{
    public int index;
    public IEnumerator routine;
    public CustomCoroutineToken token;
    public bool isNested;

    public CustomCoroutineInternal(int index, IEnumerator routine, CustomCoroutineToken token, bool isNested)
    {
        this.index = index;
        this.routine = routine;
        this.token = token;
        this.isNested = isNested;
    }
    public CustomCoroutineInternal Init(int index, IEnumerator routine, CustomCoroutineToken token, bool isNested)
    {
        this.index = index;
        this.routine = routine;
        this.token = token;
        this.isNested = isNested;
        return this;
    }

    public CustomCoroutineToken OnStart() => token.OnStart();
    public CustomCoroutineToken OnAsync() => token.OnAsync();
    public CustomCoroutineToken OnSync() => token.OnSync();
    public CustomCoroutineToken OnPause() => token.OnPause();
    public CustomCoroutineToken OnStop() => token.OnStop();
    public bool KeepWaiting() => token.ReturnNull;
    public bool UpdateWaiting() => token.KeepWaiting();
}