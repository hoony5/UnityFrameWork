using System.Collections.Generic;
using System.Linq;
using Share;

namespace Diagram
{
    public sealed class MethodFieldSection : FieldSection<MethodInfo, MethodSectionSubView>, ISectionFactory
    {
        private readonly IDiagramNode node;
        public readonly MethodSectionView view;
        private List<MethodSectionSubView> subviewItems;
        private MethodSectionViewBinder sectionViewBinder;
        private List<MethodSectionSubViewBinder> subviewItemBinders;
        public MethodFieldSection(IDiagramNode node, string title)
        {
            this.node = node;
            view = new MethodSectionView(title);
            subviewItems = new List<MethodSectionSubView>();
            subviewItemBinders = new List<MethodSectionSubViewBinder>();
            sectionViewBinder = new MethodSectionViewBinder(view);
        }
        public override void Setup()
        {
            switch (node)
            {
                case DiagramNormalNode:
                    sectionViewBinder.BindAddButton(AddNormalNodeField);
                    break;
                case DiagramEventNode:
                    sectionViewBinder.BindAddButton(AddEventNodeField);
                    break;
            }
        }
        
        public override void SetVisible(bool visible)
        {
            view.foldout.SetDisplay(visible);
        }

        public override void ConvertToStatic()
        {
            if (subviewItemBinders == null || subviewItemBinders.Count == 0)
                return;

            subviewItemBinders.ForEach(binder => binder.ConvertToStatic());
        }

        public override void Dispose()
        {
            sectionViewBinder.DisposeAddButton(AddNormalNodeField);
            DisposeSubViews();
            RemoveSubviewElements();
        }

        public override void DisposeSubViews()
        {
            if (subviewItemBinders == null || subviewItemBinders.Count == 0)
                return;

            subviewItemBinders.ForEach(binder => binder.Dispose());
            subviewItemBinders.Clear();
            subviewItems.Clear();
        }
        public override void Load()
        {
            if (node.NodeModel.Header.Methods.Count == 0)
                return;
            
            node.NodeModel.Header.Methods?.For((index, fieldInfo) =>
            {
                switch (node)
                {
                    case DiagramNormalNode:
                        LoadNormalNodeField(fieldInfo);
                        break;
                    case DiagramEventNode:
                        LoadEventNodeField(fieldInfo);
                        break;
                }
            });
        }

        public override void Reload()
        {
            DisposeSubViews();
            Load();
        }

        protected override void Bind(MethodInfo fieldInfo, MethodSectionSubView subview)
        {
            MethodSectionSubViewBinder binder 
                = new MethodSectionSubViewBinder(node, subview, fieldInfo);
            
            binder.Bind();
            subviewItemBinders.Add(binder);
            
            binder.BindRemoveButton(() =>
            {
                binder.Dispose();
                subviewItemBinders.Remove(binder);
                RemoveSubviewItem(subview);
                node.ModelModifier.RemoveMethod(fieldInfo);
                
            });
        }
        
        protected override void UpdateFieldInfo(MethodInfo fieldInfo)
        {
            node.ModelModifier.AddOrUpdateMethod(fieldInfo);
        }

        protected override void AddEventNodeField()
        {
            MethodSectionSubView subview = view.CreateEventNodeView(nameFieldLabel:"Method", predicateFieldLabel:"Predicate - None");
            subviewItems.Add(subview);
            node.extensionContainer.SetWidth(subview.nameTextField.style.width);
            
            // bind to Model
            MethodInfo fieldInfo = new MethodInfo(node.NodeModel.Header.Name) { AccessType = "private" };
            UpdateFieldInfo(fieldInfo);
            Bind(fieldInfo, subview);
        }
        protected override void AddNormalNodeField()
        {
            MethodSectionSubView subview = view.CreateNormalNodeSubview(
                "Public",
                "NonModified",
                "ReturnType",
                "Name",
                "Parameters");
            subviewItems.Add(subview);
            
            node.extensionContainer.SetWidth(subview.parameterTextField.style.width);
            // bind to Model
            MethodInfo fieldInfo = new MethodInfo(node.NodeModel.Header.Name) { AccessType = "public" };
            
            UpdateFieldInfo(fieldInfo);
            Bind(fieldInfo, subview);
        }
        protected override void LoadEventNodeField(MethodInfo fieldInfo)
        {
            MethodSectionSubView subview = view.CreateEventNodeView(fieldInfo.Name, fieldInfo.Predicate);
            subviewItems.Add(subview);
            // bind to Model
            UpdateFieldInfo(fieldInfo);
            Bind(fieldInfo, subview);
        }
        protected override void LoadNormalNodeField(MethodInfo fieldInfo)
        {
            MethodSectionSubView subview = view.CreateNormalNodeSubview(
                fieldInfo.AccessType,
                fieldInfo.DeclarationType.ToString(),
                fieldInfo.Type,
                fieldInfo.Name,
                string.Join(", ",
                    fieldInfo.Parameters
                        .Select(p => $"{(p.name.IsNullOrEmpty() ? p.type : $"{p.type} {p.name}")}")
                        .ToArray())
                );
            subviewItems.Add(subview);
            node.extensionContainer.SetWidth(subview.parameterTextField.style.width);
            // bind to Model
            UpdateFieldInfo(fieldInfo);
            Bind(fieldInfo, subview);
        }

        protected override void RemoveSubviewItem(MethodSectionSubView subviewToRemove)
        {
            if (!subviewItems.Any(subviewToRemove.IsMatch)) return;
            subviewToRemove.group.RemoveFromHierarchy();
            subviewItems.Remove(subviewToRemove);
        }

        protected override int GetSubviewItemCount(MethodSectionSubView subview)
        {
            return subviewItems.Count(subview.IsMatch);
        }

        protected override  void RemoveSubviewElements()
        {
            if(subviewItems.Count == 0) return;
            subviewItems.ForEach(subView => subView.RemoveFromHierarchy());
        }
    }
}