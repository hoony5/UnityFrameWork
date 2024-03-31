using System;
using System.Linq;
using Diagram;

namespace Writer.SourceGenerator.Format.Writer
{
    public class EventCallbackWriter : ContentWriterBase
    {
        private readonly string callbackFormat;
        
        private readonly DiagramNodeModel nodeModel;
        
        public EventCallbackWriter(DiagramNodeModel nodeModel)
        {
            this.nodeModel = nodeModel;
            
            AddFormat(nameof(callbackFormat), new CallbackEventMethodFormat());
        }

        private string SubscribeCallbackToString(string note, string methodName, string callbackType)
        {
            return GetFormat(nameof(callbackFormat))
                .Replace("{0}", $"{note}")
                .Replace("{1}", "void")
                .Replace("{2}", methodName)
                .Replace("{3}", $"{callbackType!} payload");
        }
        private string PublishCallbackToString(string note, string methodName, string callbackType)
        {
            return GetFormat(nameof(callbackFormat))
                .Replace("{0}", $"{note}")
                .Replace("{1}", callbackType!)
                .Replace("{2}", methodName)
                .Replace("{3}", string.Empty);
        }
        /// <summary>
        ///  {0} : Note
        ///  {1} : Return Type
        ///  {2} : Name
        ///  {3} : Type
        /// </summary>
        /// <returns></returns>
        public override string WriteContent()
        {
            string publishCallbacks = string.Empty;
            string subscribeCallbacks = string.Empty;
            if(nodeModel.PublishingEvents.Count != 0)
            {
                publishCallbacks = string.Join('\n',
                    nodeModel.PublishingEvents.Select(x =>
                        string.Join('\n',
                            x.Methods.Select(y =>
                                $"    {PublishCallbackToString("publish callbacks", y.Name, x.Properties[0].Type)}"))));
            }
            if(nodeModel.SubscribingEvents.Count != 0)
            {
                subscribeCallbacks = string.Join('\n',
                    nodeModel.SubscribingEvents.Select(x =>
                        string.Join('\n',
                            x.Methods.Select(y =>
                                $"    {SubscribeCallbackToString("subscribe callbacks", y.Name, x.Properties[0].Type)}"))));
            }
            
            return string.Join('\n', publishCallbacks, subscribeCallbacks);
        }
    }

}