using UnityEngine;

namespace Diagram
{
    [System.Serializable]
    public class DiagramStatusModel
    {
        [field:SerializeField] public GraphElementType GraphElementType { get; set; }
        private bool isDirty;
        private bool headerIsDirty;
        public bool NeedSavingDiagram { get; set; }
        public bool HeaderIsDirty
        {
            get => headerIsDirty;
            set
            {
                if (value)
                {
                    NeedSavingDiagram = true;
                }
                headerIsDirty = value;
                IsDirty = value;
            }
        }
        public bool IsDirty
        {
            get => isDirty;
            set
            {
                if (value)
                {
                    NeedSavingDiagram = true;
                }
                isDirty = value;
            }
        }
        
        public DiagramStatusModel(GraphElementType graphElementType)
        {
            GraphElementType = graphElementType;
        }
        
        public DiagramStatusModel(DiagramStatusModel other)
        {
            GraphElementType = other.GraphElementType;
            isDirty = other.IsDirty;
            headerIsDirty = other.HeaderIsDirty;
            NeedSavingDiagram = other.NeedSavingDiagram;
        }
    }
}