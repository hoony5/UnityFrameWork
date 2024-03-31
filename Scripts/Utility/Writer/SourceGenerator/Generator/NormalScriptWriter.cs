using System.Collections.Generic;
using System.IO;
using System.Linq;
using Diagram;
using Share;
using Writer.Core;
using Writer.Ex;

namespace Writer.SourceGenerator.Format.Writer
{
    public class NormalScriptWriter : SourceExporterBase
    {
        private ScriptGeneratorSettings settings;

        private readonly IWriterFormat scriptFormat;
        private IContentWriter propertyWriter;
        private IContentWriter methodWriter;
        private IContentWriter subscribeWriter;
        private IContentWriter unsubscribeWriter;
        private IContentWriter publishWriter;
        private IContentWriter callbackWriter;
        
        private readonly DiagramNodeModel nodeModel;

        public NormalScriptWriter(DiagramNodeModel nodeModel, ScriptGeneratorSettings settings)
        {
            this.nodeModel = nodeModel;
            this.settings = settings;
            string nameSpace = nodeModel.Header.Namespace;
            string usingSpace = @"using System;
using UnityEngine;
using Newtonsoft.Json;
using SuperMaxim.Messaging;
";
            scriptFormat = new NormalScriptFormat(nameSpace, usingSpace);
            propertyWriter = new PropertyWriter(nodeModel);
            methodWriter = new MethodWriter(nodeModel);
            subscribeWriter = new SubscribingEventWriter(nodeModel);
            unsubscribeWriter = new UnsubscribingEventWriter(nodeModel);
            publishWriter = new PublishingEventWriter(nodeModel);
            callbackWriter = new EventCallbackWriter(nodeModel);
        }

        public async void Write(string rootPath)
        {
            string savePath = Path.Combine(rootPath, $"{nodeModel.Header.Name.RemoveSpace()}.cs");
            await WriteScript(savePath, GenerateScript());
        }

        private string WriteSubscribeEvents()
        {
            string subscribeContent = subscribeWriter.WriteContent();
            string subscribeParse = subscribeContent.IsNullOrEmpty()
                ? string.Empty
                : @"public void SetUpSubscribe()
    {
{0}
    }";
            return $"\t{subscribeParse.Replace("{0}", subscribeContent)}\n";
        }

        private string WriteUnsubscribeEvents()
        {
            string unsubscribeContent = unsubscribeWriter.WriteContent();
            string unsubscribeParse = unsubscribeContent.IsNullOrEmpty()
                ? string.Empty
                : @"public void ReleaseSubscribe()
    {
{0}
    }";
            return $"\t{unsubscribeParse.Replace("{0}", unsubscribeContent)}\n";
        }
        private string WritePublishEvents()
        {
            string publishContent = publishWriter.WriteContent();
            string publishParse = publishContent.IsNullOrEmpty()
                ? string.Empty
                : @"public void Publish()
    {
{0}
    }";
            return $"\t{publishParse.Replace("{0}", publishContent)}\n";
        }
        /// <summary>
        ///{0} : access
        ///{1} : declaration
        ///{2} : type
        ///{3} : inheritances
        ///{4} : properties
        ///{5} : parameters
        ///{6} : constructors
        ///{7} : methods
        ///{8} : subscribe
        ///{9} : unsubscribe
        /// {10} : publish
        /// {11} : callbacks
        /// {12} : toString
        /// </summary>
        private string GenerateScript()
        {
            string accessType = nodeModel.Header.AccessType.ToLowerString().Replace("_", " ");
            string declaration = nodeModel.Header.AccessKeyword.ToLowerString();
            if (declaration == "enum")
                return "Enum cannot be generated as a script. Please use EnumJsonConverter instead.";
            string inheritances = nodeModel.Inheritances.Count == 0
                ? string.Empty
                : $": {string.Join(", ", nodeModel.Inheritances.OrderBy(x => (int)x.AccessKeyword).Select(x => x.Name))}";
            string propertyContent = propertyWriter.WriteContent();
            string parameterContent = string.Join(", ",
                nodeModel.Header.Properties.Select(x => $"{x.Type} {x.Name.ToLowerAt(0)}"));
            string constructorContent = string.Join(", ",
                nodeModel.Header.Properties.Select(x => $"{x.Name} = {x.Name.ToLowerAt(0)};"));
            string methodContent = methodWriter.WriteContent();
            string propertiesInfos = string.Join("\n",
                nodeModel.Header.Properties.Select(x =>
                    $"* {x.Name}            :            {{{x.Name.ToLowerAt(0)}}}"));
            string callbacks = callbackWriter.WriteContent();
            string toStringContent = $@"return $@""[{nodeModel.Header.Name}]
* Properties
{propertiesInfos}"";";

            string contentFormat = scriptFormat.GetFormat();
            string content = contentFormat
                .Replace("{0}", accessType)
                .Replace("{1}", declaration)
                .Replace("{2}", nodeModel.Header.Name.RemoveSpace())
                .Replace("{3}", inheritances)
                .Replace("{4}", propertyContent)
                .Replace("{5}", parameterContent)
                .Replace("{6}", constructorContent)
                .Replace("{7}", methodContent)
                .Replace("{8}", $"{WriteSubscribeEvents()}")
                .Replace("{9}", $"{WriteUnsubscribeEvents()}")
                .Replace("{10}",$"{WritePublishEvents()}")
                .Replace("{11}", callbacks)
                .Replace("{12}", toStringContent);

            return content;
        }
    }
}