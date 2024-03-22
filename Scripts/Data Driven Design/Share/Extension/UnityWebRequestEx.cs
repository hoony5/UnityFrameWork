using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Share
{
    public static class UnityWebRequestEx
    {
        public static Task<UnityWebRequest> SendWebRequestTask(this UnityWebRequest webRequest)
        {
            TaskCompletionSource<UnityWebRequest> completionSource = new TaskCompletionSource<UnityWebRequest>();
            webRequest.SendWebRequest().completed += _ =>
            {
                completionSource.SetResult(webRequest);
            };
            return completionSource.Task;
        }
    }
}
