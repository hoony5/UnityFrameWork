using System.Collections.Generic;
using UnityEngine;
using Utility.ExcelReader;

namespace Diagram
{
    [System.Serializable]
    public class DiagramNodeModel
    {
        [SerializeField] private DiagramStatusModel status;
        [SerializeField] private DiagramHeaderModel header;
        [SerializeField] private DiagramNoteModel note;
        [SerializeField] private DiagramBlackboardModel blackboard;
        
        [SerializeField] private List<DiagramHeaderModel> inheritances;
        [SerializeField] private List<DiagramHeaderModel> subscribingEvents;
        [SerializeField] private List<DiagramHeaderModel> publishingEvents;
        [SerializeField] private List<DiagramHeaderModel> noteNodes;
        [SerializeField] private List<DiagramHeaderModel> injections;
        [SerializeField] private List<DiagramHeaderModel> members;
        
        [SerializeField] private ExcelDataSO excelData;

        public DiagramStatusModel Status
        {
            get => status;
            private set => status = value;
        }
        public DiagramHeaderModel Header
        {
            get => header;
            set
            {
                header = value;
                status.IsDirty = true;
            }
        }
        public DiagramNoteModel Note
        {
            get => note;
            private set => note = value;
        }
        public DiagramBlackboardModel Blackboard
        {
            get => blackboard;
            private set => blackboard = value;
        }
        public List<DiagramHeaderModel> Inheritances
        { 
            get => inheritances; 
            set
            {
               inheritances = value;
               status.IsDirty = true;
            } 
        }
        public List<DiagramHeaderModel> PublishingEvents
        { 
            get => publishingEvents; 
            set
            {
                publishingEvents = value;
                status.IsDirty = true;
            } 
        }
        public List<DiagramHeaderModel> SubscribingEvents
        { 
            get => subscribingEvents; 
            set
            {
                subscribingEvents = value;
                status.IsDirty = true;
            } 
        }
        public List<DiagramHeaderModel> NoteNodes
        { 
            get => noteNodes; 
            set
            {
                noteNodes = value;
                status.IsDirty = true;
            } 
        }
        public List<DiagramHeaderModel> Injections
        { 
            get => injections; 
            set
            {
                injections = value;
                status.IsDirty = true;
            } 
        }
        public List<DiagramHeaderModel> Members
        { 
            get => members; 
            set
            {
                members = value;
                status.IsDirty = true;
            } 
        }
        
        public ExcelDataSO ExcelData
        {
            get => excelData;
            set
            {
                if (value != null)
                {
                    if (excelData == null)
                    {
                        excelData = value;
                        excelData.TargetEntityTypeNames.Add(header.Name);
                    }
                    else
                    {
                        excelData.TargetEntityTypeNames.Remove(header.Name);
                        excelData = value;
                        excelData.TargetEntityTypeNames.Add(header.Name);
                    }
                    status.IsDirty = true;
                    return;
                }
                excelData = value;
                if (value == null) return;
                
                if(excelData.TargetEntityTypeNames.Contains(header.Name))
                    excelData.TargetEntityTypeNames.Remove(header.Name);

                status.IsDirty = true;
            }
        }

        public DiagramNodeModel()
        {
            status = new DiagramStatusModel(GraphElementType.Normal);
            header = new DiagramHeaderModel("New Node", Vector2.zero, status);
            note = new DiagramNoteModel("New Node", status);
            blackboard = new DiagramBlackboardModel(status);
            
            inheritances = new List<DiagramHeaderModel>();
            publishingEvents = new List<DiagramHeaderModel>();
            subscribingEvents = new List<DiagramHeaderModel>(); 
            noteNodes = new List<DiagramHeaderModel>();
            injections = new List<DiagramHeaderModel>();
            members = new List<DiagramHeaderModel>();
        }

        public DiagramNodeModel(string name, Vector2 startPosition)
        {
            status = new DiagramStatusModel(GraphElementType.Normal);
            header = new DiagramHeaderModel(name, startPosition, status);
            note = new DiagramNoteModel(name, status);
            blackboard = new DiagramBlackboardModel(status);
            
            inheritances = new List<DiagramHeaderModel>();
            publishingEvents = new List<DiagramHeaderModel>();
            subscribingEvents = new List<DiagramHeaderModel>(); 
            noteNodes = new List<DiagramHeaderModel>();
            injections = new List<DiagramHeaderModel>();
            members = new List<DiagramHeaderModel>();
        }

        public DiagramNodeModel(DiagramNodeModel other)
        {
            // receives a deep copy of the other diagram view model
            status = new DiagramStatusModel(other.status);
            header = new DiagramHeaderModel(other.header);
            note = new DiagramNoteModel(other.note);
            blackboard = new DiagramBlackboardModel(other.blackboard);
            
            inheritances = other.inheritances is null 
                ? new List<DiagramHeaderModel>(4) 
                : new List<DiagramHeaderModel>(other.inheritances);
            publishingEvents = other.subscribingEvents is null 
                ? new List<DiagramHeaderModel>(4) 
                : new List<DiagramHeaderModel>(other.publishingEvents);
            subscribingEvents = other.subscribingEvents is null 
                ? new List<DiagramHeaderModel>(4) 
                : new List<DiagramHeaderModel>(other.subscribingEvents);
            injections = other.injections is null 
                ? new List<DiagramHeaderModel>(4) 
                : new List<DiagramHeaderModel>(other.injections);
            noteNodes = other.noteNodes is null 
                ? new List<DiagramHeaderModel>(4) 
                : new List<DiagramHeaderModel>(other.noteNodes);
            members = other.members is null 
                ? new List<DiagramHeaderModel>(4) 
                : new List<DiagramHeaderModel>(other.members);
            
            ExcelData = other.ExcelData ? other.ExcelData : null;
        }
    }
}