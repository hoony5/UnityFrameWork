using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Diagram
{
    [System.Serializable]
    public class DiagramHeaderModel
    {
        [SerializeField] private DiagramStatusModel status; 
        [SerializeField] private string id;
        [SerializeField] private string oldId;
        [SerializeField] private string name;
        [SerializeField] private string group;
        [SerializeField] private string nameSpace;
        [SerializeField] private string description;
        [SerializeField] private string summarizeDescription;
        [SerializeField] private AccessType accessType;
        [SerializeField] private AccessKeyword accessKeyword;
        [SerializeField] private EntityType entityType;
        [SerializeField] private EventType eventType;
        [SerializeField] private List<PropertyFieldInfo> properties;
        [SerializeField] private List<MethodInfo> methods;
        [SerializeField] private Vector2 position;
        [SerializeField] private Vector2 size;

        public DiagramStatusModel Status
        {
            get => status;
            private set => status = value;
        }

        public string OldID
        {
            get => oldId;
            set
            {
                oldId = value;
                status.IsDirty = true;
            }
        }

        public string ID
        {
            get => id;
            set
            {
                oldId = id;
                id = value;
                status.IsDirty = true;
            }
        }

        public string Namespace
        {
            get => nameSpace;
            set
            {
                nameSpace = value;
                status.IsDirty = true;
            }
        }
        public string Name 
        { 
            get => name; 
            set
            {
                name = value;
                status.IsDirty = true;
                status.HeaderIsDirty = true;
            } 
        }
        public string Group 
        { 
            get => group; 
            set
            {
                group = value;
                status.IsDirty = true;
            } 
        }
        
        public string Description 
        { 
            get => description; 
            set
            {
                description = value;
                status.IsDirty = true;
            } 
        }
        
        public string SummarizeDescription 
        { 
            get => summarizeDescription; 
            set
            {
                summarizeDescription = value;
                status.IsDirty = true;
            } 
        }

        public AccessType AccessType
        {
            get => accessType;
            set
            {
                if(accessType == value) return;
                accessType = value;
                status.IsDirty = true;  
                status.HeaderIsDirty = true;
            } 
        }
        public AccessKeyword AccessKeyword 
        { 
            get => accessKeyword; 
            set
            {
                if(accessKeyword == value) return;
                accessKeyword = value;
                status.IsDirty = true;
                status.HeaderIsDirty = true;
            } 
        }
        public EntityType EntityType 
        { 
            get => entityType; 
            set
            {
                if(entityType == value) return;
                entityType = value;
                status.IsDirty = true;
                status.HeaderIsDirty = true;
            } 
        }
        public EventType EventType 
        { 
            get => eventType; 
            set
            {
                if(eventType == value) return;
                eventType = value;
                status.IsDirty = true;
                status.HeaderIsDirty = true;
            } 
        }
        public List<PropertyFieldInfo> Properties
        { 
            get => properties; 
            set
            {
                properties = value;   
                status.IsDirty = true;
            } 
        }
        public List<MethodInfo> Methods 
        { 
            get => methods; 
            set
            {
                methods = value;
                status.IsDirty = true;
            } 
        }
        public Vector2 Position 
        { 
            get => position; 
            set
            {
                // if the position is the same, do not set the value
                if(position.Approximately(value)) return;
                position = value;
                status.IsDirty = true;
            } 
        }
        public Vector2 Size 
        { 
            get => size; 
            set
            {
                // if the position is the same, do not set the value
                if(size.Approximately(value)) return;
                size = value;
                status.IsDirty = true;
            } 
        }


        public DiagramHeaderModel(DiagramStatusModel statusModel)
        {
            this.status = statusModel; 
            ID = Guid.NewGuid().ToString();
            Namespace = "";
            AccessType = AccessType.Public;
            AccessKeyword = AccessKeyword.Class;
            Name = "New Class";
            EntityType = EntityType.None;
            Group = "";
            Description = "";
            SummarizeDescription = "";
            EventType = EventType.Not_Used;
            Properties = new List<PropertyFieldInfo>(4);
            Methods = new List<MethodInfo>(4);
            Position = Vector2.zero;
            Size = Vector2.zero;
        }
        public DiagramHeaderModel(string name , Vector2 position, DiagramStatusModel status)
        {
            this.status = status;
            ID = Guid.NewGuid().ToString();
            Namespace = "";
            AccessType = AccessType.Public;
            AccessKeyword = AccessKeyword.Class;
            Name = name;
            EntityType = EntityType.None;
            Group = "None";
            Description = "";
            SummarizeDescription = "";
            EventType = EventType.Not_Used;
            Properties = new List<PropertyFieldInfo>(4);
            Methods = new List<MethodInfo>(4);
            Position = position;
            Size = Vector2.zero;
        }

        public DiagramHeaderModel(DiagramHeaderModel other)
        {
            status = other.status;
            ID = other.ID;
            Namespace = other.Namespace;
            AccessType = other.AccessType;
            AccessKeyword = other.AccessKeyword;
            Name = other.Name;
            EntityType = other.EntityType;
            Group = other.Group;
            Description = other.Description;
            SummarizeDescription = other.SummarizeDescription;
            EventType = other.EventType;
            Properties = new List<PropertyFieldInfo>(other.Properties);
            Methods = new List<MethodInfo>(other.Methods);
            Position = other.Position;
            Size = other.Size;
        }
    }
}