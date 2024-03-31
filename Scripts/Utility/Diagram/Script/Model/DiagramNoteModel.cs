using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Diagram
{
    [System.Serializable]
    public class DiagramNoteModel
    {
        [SerializeField] private DiagramStatusModel status;
        [SerializeField] private ExportFileType exportFileType;
        [SerializeField] private DescriptionType descriptionType;
        // main
        [SerializeField] private string title;
        // sub title / note
        [SerializeField] private string subTitle;
        [SerializeField] private string description;
        // summary / detail
        [SerializeField] private string summary;
        [SerializeField] private string detail;
        // check list
        [SerializeField] private List<CheckListInfo> checkList;  // ex. "description.value - toggle.value, description.value - toggle.value, ..."
        // table
        [SerializeField] private SerializedDictionary<int, List<CellInfo>> cells;
        
        // default > sub title / note
        // summary > summary / detail
        // check list > check list
        // table > table
        
        public DiagramStatusModel Status
        {
            get => status;
            private set => status = value;
        }
        public string Title 
        { 
            get => title; 
            set
            {
                if(title == value) return;
                title = value;
                status.IsDirty = true;
                status.HeaderIsDirty = true;
            } 
        }
        public ExportFileType ExportFileType 
        { 
            get => exportFileType; 
            set
            {
                if(exportFileType == value) return;
                exportFileType = value;
                status.IsDirty = true;
                status.HeaderIsDirty = true;
            } 
        }
        public DescriptionType DescriptionType 
        { 
            get => descriptionType; 
            set
            {
                if(descriptionType == value) return;
                descriptionType = value;
                status.IsDirty = true;
                status.HeaderIsDirty = true;
            } 
        }
        public string SubTitle 
        { 
            get => subTitle; 
            set
            {
                if(subTitle == value) return;
                subTitle = value;
                status.IsDirty = true;
                status.HeaderIsDirty = true;
            } 
        }
        public string Description 
        { 
            get => description; 
            set
            {
                if(description == value) return;
                description = value;
                status.IsDirty = true;
            } 
        }
        public string Summary 
        { 
            get => summary; 
            set
            {
                if(summary == value) return;
                summary = value;
                status.IsDirty = true;
            } 
        }
        public string Detail 
        { 
            get => detail; 
            set
            {
                if(detail == value) return;
                detail = value;
                status.IsDirty = true;
            } 
        }
        public List<CheckListInfo> CheckList 
        { 
            get => checkList; 
            set
            {
                if(checkList == value) return;
                checkList = value;
                status.IsDirty = true;
            } 
        }
        public SerializedDictionary<int, List<CellInfo>> Cells 
        { 
            get => cells; 
            set
            {
                if(cells == value) return;
                cells = value;
                status.IsDirty = true;
            } 
        }
        public DiagramNoteModel(DiagramNoteModel other)
        {
            status = other.status;
            Title = other.Title;
            SubTitle = other.SubTitle;
            ExportFileType = other.ExportFileType;
            DescriptionType = other.DescriptionType;
            Description = other.Description;
            Summary = other.Summary;
            Detail = other.Detail;
            CheckList = other.CheckList;
            Cells = other.Cells;
        }
        public DiagramNoteModel(string title, DiagramStatusModel status)
        {
            this.status = status;
            Title = title;
            SubTitle = "";
            ExportFileType = ExportFileType.Text;
            DescriptionType = DescriptionType.Default;
            Description = "";
            Summary = "";
            Detail = "";
            checkList = new List<CheckListInfo>();
            cells = new SerializedDictionary<int, List<CellInfo>>();
        }
    }
}