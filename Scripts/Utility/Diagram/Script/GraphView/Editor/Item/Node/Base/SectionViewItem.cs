using UnityEngine.UIElements;

namespace Diagram
{
    public abstract class SectionViewItem
    {
        public VisualElement group;
        
        public void RemoveFromHierarchy()
        {
            group.RemoveFromHierarchy();
        }
    }
}