using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Share;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class BlackboardConfluenceBinder
    {
        private DiagramConfig config;
        private BlackboardNoteNodeView view;
        public DiagramNoteNode node;
        private HttpClient httpClient;
        public BlackboardConfluenceBinder(BlackboardNoteNodeView view, DiagramConfig config)
        {
            this.view = view;
            this.config = config;
        }
        private void ConfluenceClient()
        {
            httpClient = new HttpClient();
            byte[] byteArray = Encoding.ASCII.GetBytes($"{config.confluence.username}:{config.confluence.apiToken}"); 
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        private async Task AddPageAsync(string headLine, string content)
        {
            string url = $"{config.confluence.url}/rest/api/content";

            dynamic newPageData = new
            {
                type = "page",
                title = headLine,
                space = new { key = config.confluence.spaceKey },
                body = new
                {
                    storage = new
                    {
                        value = content,
                        representation = "wiki"
                    }
                }
            };

            dynamic jsonData = JsonConvert.SerializeObject(newPageData);
            HttpResponseMessage response =
                await httpClient.PostAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                Debug.Log($@"Failed to add page: {response.ReasonPhrase}
req : {response.RequestMessage.Content.ReadAsStringAsync().Result}
res : {response.Content.ReadAsStringAsync().Result}");
                return;
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            dynamic pageData = JsonConvert.DeserializeObject(jsonResponse);
            if (pageData == null) return;
            dynamic pageId = pageData.id;
            node.NodeModel.Blackboard.PageID = pageId;
            Debug.Log($@"New page ID: {pageId}
Confluence Add Response : {response.StatusCode}");
        }

        public async Task UpdatePageAsync(long pageId, string newTitle, string newContent)
        {
            string url = $"{config.confluence.url}/rest/api/content/{pageId}";
            int currentVersion = await GetCurrentPageVersionAsync(pageId.ToString());
            if (currentVersion == int.MinValue)
            {
                Debug.Log("Failed to get current page version");
                return;
            }

            dynamic updatePageData = new
            {
                version = new { number = currentVersion + 1 },
                title = newTitle,
                type = "page",
                ancestors = config.confluence.ancestors.Select(id => new { id }).ToArray(),
                space = new { key = config.confluence.spaceKey },
                body = new
                {
                    storage = new
                    {
                        value = newContent,
                        representation = "wiki"
                    }
                }
            };

            dynamic jsonData = JsonConvert.SerializeObject(updatePageData);
            HttpResponseMessage response =
                await httpClient.PutAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                Debug.Log($@"Failed to update page: {response.ReasonPhrase}
req : {response.RequestMessage.Content.ReadAsStringAsync().Result}
res : {response.Content.ReadAsStringAsync().Result}");
                return;
            }

            Debug.Log("Page updated successfully");
        }

        private async Task<string> GetPageIdAsync(string pageTitle)
        {
            string url =
                $"{config.confluence.url}/rest/api/content?title={Uri.EscapeDataString(pageTitle)}&spaceKey={config.confluence.spaceKey}&expand=history,lastUpdated";
            Debug.Log($"Get Page ID : {url}");
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return string.Empty;

            string content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);

            if (result == null) return string.Empty;

            foreach (dynamic page in result.results)
            {
                if (page.title == pageTitle)
                {
                    return page.id;
                }
            }

            return string.Empty;
        }

        private async Task<int> GetCurrentPageVersionAsync(string pageId)
        {
            var url = $"{config.confluence.url}/rest/api/content/{pageId}?expand=version";
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return int.MinValue;

            string content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            int currentVersion = (int)json["version"]?["number"];
            return currentVersion;
        }

        private void BindGetPageID()
        {
            if (node.NodeModel.Note.ExportFileType != ExportFileType.Confluence) return;
            ConfluenceClient();
            string pageID = string.Empty;
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            Task getPageAsync = Task.Run(async () =>
            {
                if (view.confluence.confluenceCurrentPageTitleToggle.value)
                    pageID = await GetPageIdAsync(node.NodeModel.Note.Title);
                else
                    pageID = await GetPageIdAsync(view.confluence.confluencePageTitleField.value);

                tcs.SetResult(pageID);
            });
            tcs.Task.Wait();
            if (getPageAsync.IsCompleted)
            {
                node.NodeModel.Blackboard.PageID = pageID;
                Debug.Log($"Get Page ID Result : {(pageID.IsNullOrEmpty() ? "Failed : Page ID is Empty" : "Success")}");
            }
            else
            {
                Debug.Log($"Get Page ID Result : {getPageAsync.Status}");
            }
        }
        private void BindGetPage()
        {
            _ = Task.Run(GetPageByTitleAsync);
        }

        private async Task<string> GetPageByTitleAsync()
        {
            string pageTitle = view.confluence.confluencePageTitleField.value;
            Task<string> getPageID = GetPageIdAsync(pageTitle);
            Task.WaitAll(getPageID);

            if (getPageID.Result.IsNullOrEmpty())
            {
                Debug.Log($"Failed to get page by title | {pageTitle}");
                return string.Empty;
            }

            string url =
                $"{config.confluence.url}/rest/api/content?title={Uri.EscapeDataString(pageTitle)}&spaceKey={config.confluence.spaceKey}&expand=body.storage";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content; // JSON response containing page details
            }

            Debug.Log($"Failed to get page by title: {response.ReasonPhrase}");
            return string.Empty;
        }

        // TODO :: Add option , storage vs wiki vs atlassian_document
        // TODO :: Add option , markup wiki vs storage
        public async Task SendMessageToConfluence()
        {
            if (node.NodeModel.Note.ExportFileType != ExportFileType.Confluence) return;
            ConfluenceClient();

            bool isCustomTitle = view.confluence.confluenceCurrentPageTitleToggle.value;
            string title = isCustomTitle
                ? node.NodeModel.Note.Title
                : view.confluence.confluencePageTitleField.value;
            bool hasAncestor = title.Contains(config.confluence.ancestorSplitter);
            
            if (hasAncestor)
            {
                string[] titles = title.Split(config.confluence.ancestorSplitter);
                title = titles[^1];
                // []
                config.confluence.ancestors = titles[..^1];
            }
            else
            {
                config.confluence.ancestors = Array.Empty<string>();
            }
            
            string message = node.NodeModel.Blackboard.ExportExampleNote;

            if (node.NodeModel.Blackboard.PageID.IsNullOrEmpty())
            {
                node.NodeModel.Blackboard.PageID = await GetPageIdAsync(title);
            }

            if (node.NodeModel.Blackboard.PageID.IsNullOrEmpty())
            {
                await AddPageAsync(title, message);
                return;
            }

            long pageID = long.TryParse(node.NodeModel.Blackboard.PageID, out long id) ? id : long.MaxValue;
            await UpdatePageAsync(pageID, title, message);
        }

        private void BindConfluenceLink(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.ConfluenceBaseUrl = evt.newValue;
            if (!view.useGlobalConfig.value) return;
            config.confluence.url = evt.newValue;
        }
        private void BindConfluenceUserName(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.ConfluenceUserName = evt.newValue;
            if (!view.useGlobalConfig.value) return;
            config.confluence.username = evt.newValue;
        }
        private void BindConfluenceAPIToken(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.ConfluenceAPIToken = evt.newValue;
            if (!view.useGlobalConfig.value) return;
            config.confluence.apiToken = evt.newValue;
        }

        private void BindConfluenceSpaceKey(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.ConfluenceSpaceKey = evt.newValue;
            if (!view.useGlobalConfig.value) return;
            config.confluence.spaceKey = evt.newValue;
        }
        private void OpenConfluenceHomepage()
        {
            string url = "https://www.atlassian.com/software/confluence";
            Application.OpenURL(url);
        }

        private void OpenAtlassianAPITokenPage()
        {
            string url = "https://id.atlassian.com/manage-profile/security/api-tokens";
            Application.OpenURL(url);
        }
        private void OpenConfluenceSpacePage()
        {
            string url = $"https://{config.confluence.username}.atlassian.net/wiki/spaces/{config.confluence.spaceKey}/overview";
    
            Application.OpenURL(url);
        }

        public void BindTitleSettingToggle(DiagramBlackboard blackboard)
        {
            blackboard.getConfluenceTitleTask = blackboard.schedule.Execute(() =>
            {
                if (node == null) return;
                if (node.NodeModel.Note.ExportFileType != ExportFileType.Confluence) return;
                
                view.confluence.confluencePageTitleField.value = view.confluence.confluenceCurrentPageTitleToggle.value
                    ? node.NodeModel.Note.Title
                    : view.confluence.confluencePageTitleField.value;
                
            }).Every(250);
        }
        private void BindPageTitle(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.PageTitle = evt.newValue;
        }

        public void Load()
        {
            view.confluence.confluenceLinkField.value = node.NodeModel.Blackboard.ConfluenceBaseUrl;
            view.confluence.confluenceUserNameField.value = node.NodeModel.Blackboard.ConfluenceUserName;
            view.confluence.confluenceAPITokenField.value = node.NodeModel.Blackboard.ConfluenceAPIToken;
            view.confluence.confluenceSpaceKeyField.value = node.NodeModel.Blackboard.ConfluenceSpaceKey;
            view.confluence.confluencePageTitleField.value = view.confluence.confluenceCurrentPageTitleToggle.value
                ? node.NodeModel.Note.Title
                : node.NodeModel.Blackboard.PageTitle;
        }

        public void Bind()
        {
            view.confluence.confluenceLinkField.RegisterValueChangedCallback(BindConfluenceLink);
            view.confluence.confluenceUserNameField.RegisterValueChangedCallback(BindConfluenceUserName);
            view.confluence.confluenceAPITokenField.RegisterValueChangedCallback(BindConfluenceAPIToken);
            view.confluence.confluenceSpaceKeyField.RegisterValueChangedCallback(BindConfluenceSpaceKey);
            
            view.confluence.confluenceLinkButton.clicked += OpenConfluenceHomepage;
            view.confluence.confluenceAPITokenLinkButton.clicked += OpenAtlassianAPITokenPage;
            view.confluence.confluenceSpaceKeyLinkButton.clicked += OpenConfluenceSpacePage;
            view.confluence.confluencePageIDLoadButton.clicked += BindGetPageID;
            view.confluence.confluenceOpenPageButton.clicked += BindGetPage;
            view.confluence.confluencePageTitleField.RegisterValueChangedCallback(BindPageTitle);
        }

        public void Dispose()
        {
            view.confluence.confluenceLinkField.UnregisterValueChangedCallback(BindConfluenceLink);
            view.confluence.confluenceUserNameField.UnregisterValueChangedCallback(BindConfluenceUserName);
            view.confluence.confluenceAPITokenField.UnregisterValueChangedCallback(BindConfluenceAPIToken);
            view.confluence.confluenceSpaceKeyField.UnregisterValueChangedCallback(BindConfluenceSpaceKey);
            
            view.confluence.confluenceLinkButton.clicked -= OpenConfluenceHomepage;
            view.confluence.confluenceAPITokenLinkButton.clicked -= OpenAtlassianAPITokenPage;
            view.confluence.confluenceSpaceKeyLinkButton.clicked -= OpenConfluenceSpacePage;
            view.confluence.confluencePageIDLoadButton.clicked -= BindGetPageID;
            view.confluence.confluenceOpenPageButton.clicked -= BindGetPage;
            view.confluence.confluencePageTitleField.UnregisterValueChangedCallback(BindPageTitle);
            
            httpClient?.Dispose();
        }
    }
}