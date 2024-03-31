using System.Collections.Generic;

namespace Writer.SourceGenerator
{
    public class SourceCodeModel
    {
        /// <summary>
        /// script Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// virtual or abstract or static or sealed or string.Empty or override
        /// </summary>
        public AccessConstraint AccessConstraint { get; set; }
        /// <summary>
        /// NameSpace
        /// </summary>
        public string NameSpace { get; set; }
        /// <summary>
        /// data structures
        /// </summary>
        public DataType DataType { get; set; }
        /// <summary>
        /// interface or class
        /// </summary>
        public List<string> Inheritances { get; set; }
        /// <summary>
        /// Properties
        /// </summary>
        public List<PropertyModel> Properties { get; set; }
        
        public List<MethodModel> Methods { get; set; }
        public List<EventCallbackModel> EventCallback { get; set; }
    }

    public class PropertyModel
    {
        /// <summary>
        /// virtual or abstract or static or sealed or string.Empty or override
        /// </summary>
        public AccessConstraint AccessConstraint { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    
    }

    public class MethodModel
    {
        /// <summary>
        /// virtual or abstract or static or sealed or string.Empty or override
        /// </summary>
        public AccessConstraint AccessConstraint { get; set; }
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public List<ParameterModel> Parameters { get; set; }
    }

    public class ParameterModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class EventCallbackModel
    {
        public string Name { get; set; }
        public EventType EventType { get; set; }
        public string CallbackType { get; set; }
        public List<string> Parameters { get; set; }
        public MethodModel Predicate { get; set; }
    }

    public enum DataType
    {
        Class,
        Interface,
        Enum,
        Struct,
        Record
    }

    public enum EventType
    {
        Subscribe,
        Publish
    }
    public enum AccessConstraint
    {
        None,
        Virtual,
        Abstract,
        Static,
        Override
    }
}