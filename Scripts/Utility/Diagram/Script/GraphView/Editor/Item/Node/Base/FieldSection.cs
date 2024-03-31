using UnityEngine.UIElements;

namespace Diagram
{
    public abstract class FieldSection<TFieldInfo, TFieldViewItem>
    {
        protected VisualElement Container { get; set; }
        public Foldout Foldout { get; protected set; }
        public abstract void Setup();

        public abstract void SetVisible(bool visible);

        public abstract void ConvertToStatic();

        public abstract void Dispose();

        public abstract void DisposeSubViews();

        public abstract void Load();

        public abstract void Reload();
        protected abstract void Bind(TFieldInfo fieldInfo, TFieldViewItem subviewItem);
        
        protected abstract void UpdateFieldInfo(TFieldInfo fieldInfo);
        protected abstract void AddNormalNodeField();
        protected abstract void LoadEventNodeField(TFieldInfo fieldInfo);
        protected abstract void LoadNormalNodeField(TFieldInfo fieldInfo);
        protected abstract void RemoveSubviewItem(TFieldViewItem subviewItem);
        protected abstract int GetSubviewItemCount(TFieldViewItem subviewItem);
        protected abstract void RemoveSubviewElements();
        protected virtual void AddEventNodeField(TFieldInfo fieldInfo)
        {
            
        }
        protected virtual void AddEventNodeField()
        {
            
        }
    }
}