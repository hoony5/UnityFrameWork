using System;
using System.Threading;
using System.Threading.Tasks;

namespace Share
{
    /*
     *  safe from deadlock, track progress, and cancel, process last work nevertheless task passed 
     */
    public static class TaskEx
    {
        
        public static async Task<T> WithProgress<T>(this Task<T> task, IProgress<float> progress)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            await task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    tcs.TrySetException(t.Exception);
                }
                else if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(t.Result);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            while (!task.IsCompleted)
            {
                progress.Report(0.5f);
                await Task.Delay(100);
            }
            return await tcs.Task;
        }
        
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }
            return await task;
        }
        
        public static async Task<T> WithTimeout<T>(this Task<T> task, int timeout)
        {
            using (CancellationTokenSource cts = new CancellationTokenSource(timeout))
            {
                return await task.WithCancellation(cts.Token);
            }
        }

        public static async void RefreshDelayedEditor(
#if UNITY_EDITOR
        UnityEditor.ImportAssetOptions option = UnityEditor.ImportAssetOptions.Default
#endif
            )
        {
            await Task.Delay(500);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh(option);
#endif
        }
    }
}