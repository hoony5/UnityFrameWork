using System.Collections;
using System.Runtime.InteropServices;

public class CustomCoroutineInternal
{
    public int index;
    public string tag;
    public IEnumerator routine;
    public CustomCoroutineToken token;

    public CustomCoroutineInternal(int index, string tag, IEnumerator routine, CustomCoroutineToken token)
    {
        this.index = index;
        this.tag = tag;
        this.routine = routine;
        this.token = token;
    }

    public CustomCoroutineToken OnStart() => token.OnStart();
    public CustomCoroutineToken OnAsync() => token.OnAsync();
    public CustomCoroutineToken OnSync() => token.OnSync();
    public CustomCoroutineToken OnPause() => token.OnPause();
    public CustomCoroutineToken OnStop() => token.OnStop();
    public bool KeepWaiting() => token.ReturnNull;
    public bool UpdateWaiting() => token.KeepWaiting();
}