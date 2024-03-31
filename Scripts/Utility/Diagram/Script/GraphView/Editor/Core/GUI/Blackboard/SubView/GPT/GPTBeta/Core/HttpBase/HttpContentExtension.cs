using System.Net.Http;
using Newtonsoft.Json;

namespace GPT
{
    public static class HttpContentExtension
    {
        public static HttpContent ToHttpContent<T>(this T @object)
        {
            return new StringContent(JsonConvert.SerializeObject(@object), System.Text.Encoding.UTF8, "application/json");
        }
        
        /*public MultipartFormDataContent GetMultipartContent(string jsonContent)
        {
            MultipartFormDataContent content = new MultipartFormDataContent
            {
                { new StringContent(purpose), "purpose" },
                { new ByteArrayContent(await System.IO.File.ReadAllBytesAsync(filePath)), "file", Path.GetFileName(filePath) }
            };
            return content;
        }*/
    }

}