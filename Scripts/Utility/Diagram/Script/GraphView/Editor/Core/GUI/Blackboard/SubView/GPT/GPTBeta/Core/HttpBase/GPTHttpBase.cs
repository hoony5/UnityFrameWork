using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace GPT
{
    public class GPTHttpBase
    {
        private string apiKey;

        public GPTHttpBase(string apiKey)
        {
            this.apiKey = apiKey;
        }
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            client.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v1");
            return client;
        }

        public async Task<string> GetAsync(string url)
        {
            HttpResponseMessage res = await HttpRequest(HttpMethod.Get, default, url);
            string response = await res.Content.ReadAsStringAsync();
            return response;
        }
        public async Task<string> PostAsync(string url, HttpContent content)
        {
            HttpResponseMessage res = await HttpRequest(HttpMethod.Post, content, url);
            var response = await res.Content.ReadAsStringAsync();
            // stream async to IEnumerableAsync string 
            return response;
        }
        public async Task<string> DeleteAsync(string url)
        {
            HttpResponseMessage res = await HttpRequest(HttpMethod.Delete, null, url);
            string response = await res.Content.ReadAsStringAsync();
            return response;
        }
        
        private async Task<HttpResponseMessage> HttpRequest(HttpMethod verb, object content, string url)
        {
            if (verb == null)
                verb = HttpMethod.Get;
            
            using HttpClient client = GetClient();
            
            HttpRequestMessage req = new HttpRequestMessage(verb, url);
            if (content != null)
            {
                if (content is HttpContent httpContent)
                {
                    req.Content = httpContent;
                }
            
                if (content is not HttpContent)
                {
                    string jsonContent = JsonConvert.SerializeObject(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    req.Content = stringContent;
                }   
            }
            HttpResponseMessage res = await client.SendAsync(req, HttpCompletionOption.ResponseContentRead);
            Debug.Log($"Response : {res}");
            
            if(res.IsSuccessStatusCode) return res;
            string failMessage = await res.Content.ReadAsStringAsync();
            throw new HttpRequestException($@"{verb.ToString().Replace("HttpMethod.", string.Empty)}! - {res.StatusCode}
{failMessage}");
             
        }
        
        
    }

}