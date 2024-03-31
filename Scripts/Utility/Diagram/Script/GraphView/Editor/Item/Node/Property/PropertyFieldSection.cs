using System.Collections.Generic;
using System.Linq;
using Share;
using UnityEngine;

namespace Diagram
{
    public sealed class PropertyFieldSection : FieldSection<PropertyFieldInfo, PropertySectionSubView>, ISectionFactory
    {
        private readonly IDiagramNode node;
        public readonly PropertySectionView view;
        public readonly List<PropertySectionSubView> subviewItems;

        private readonly PropertySectionViewBinder sectionViewBinder;
        private readonly List<PropertySectionSubViewBinder> subviewItemBinders;
        public PropertyFieldSection(IDiagramNode node, string title)
        {
            this.node = node;
            view = new PropertySectionView(node, title);
            subviewItems = new List<PropertySectionSubView>();

            sectionViewBinder = new PropertySectionViewBinder(view);
            subviewItemBinders = new List<PropertySectionSubViewBinder>();
        }
        public override void Setup()
        {
            switch (node)
            {
                case DiagramNormalNode:
                    sectionViewBinder.BindAddButton(AddNormalNodeField);
                    break;
                case DiagramEventNode:
                    AddEventNodeField(new PropertyFieldInfo());
                    break;
            }
        }

        // already implemented properties & methods convert to enum section 
        public void ConvertToEnumSection(bool visible)
        {
            if (subviewItemBinders == null || 
                subviewItemBinders.Count == 0)                 
                return;

            subviewItemBinders.ForEach(
                binder => binder.ConvertToEnumSection(visible));
        }
        
        public override void Dispose()
        {
            sectionViewBinder.DisposeAddButton(AddNormalNodeField);
            DisposeSubViews();
            RemoveSubviewElements();
        }

        public override void DisposeSubViews()
        {
            if (subviewItemBinders == null || 
                subviewItemBinders.Count == 0)
                return;

            subviewItemBinders.ForEach(binder => binder.Dispose());
            subviewItemBinders.Clear();
            subviewItems.Clear();
        }

        public override void SetVisible(bool visible)
        {
            view.foldout.SetDisplay(visible);
        }

        public override void ConvertToStatic()
        {
            if (subviewItemBinders == null || 
                subviewItemBinders.Count == 0)
                return;
            
            subviewItemBinders.ForEach(binder => binder.ConvertToStatic());
        }
        
        public override void Load()
        {
            if(node.NodeModel.Header.Properties.Count == 0) return;

            node.NodeModel.Header.Properties?.For((index, fieldInfo) =>
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
        
        public void ReloadInjections(string old, string @new)
        {
            if (subviewItemBinders == null || 
                subviewItemBinders.Count == 0)
                return;

            subviewItemBinders.ForEach(binder =>
            {
                if (!binder.subView.nameTextField.text.Equals(old) ||
                    !binder.subView.typeTextField.text.Equals(old))
                    return;

                binder.fieldInfo.Name = @new;
                binder.fieldInfo.Type = @new;
                binder.subView.nameTextField.value = @new;
                binder.subView.typeTextField.value = @new;
                node.NodeModel.Status.IsDirty = true;
            });
        }

        protected override void Bind(PropertyFieldInfo fieldInfo, PropertySectionSubView subview)
        {
            PropertySectionSubViewBinder binder 
                = new PropertySectionSubViewBinder(node, subview, fieldInfo);
            
            binder.Bind();
            subviewItemBinders.Add(binder);
            
            binder.BindRemoveButton(() =>
            {
                binder.Dispose();
                subviewItemBinders.Remove(binder);
                RemoveSubviewItem(subview);
                node.ModelModifier.RemoveProperty(fieldInfo);
                
                node.GUI.gptBoard.viewModel.DrawTreeView();
                node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
            });
        }

        protected override void UpdateFieldInfo(PropertyFieldInfo fieldInfo)
        {
            node.ModelModifier.AddOrUpdateProperty(fieldInfo);
        }
        public void AddPropertyField(string type, string name)
        {
            bool isEnum = node.NodeModel.Header.AccessKeyword == AccessKeyword.Enum;
            
            PropertySectionSubView subview = view.CreateNormalNodeSubview(
                "NonModified",
                $"{(isEnum ? $"Name_{subviewItemBinders.Count}" : "object")}",
                $"{(isEnum ? $"{subviewItemBinders.Count}" : "Property")}"
            );
            subviewItems.Add(subview);
            subview.declarationTypeRepeatButton.SetDisplay(!isEnum);
            // bind to Model
            PropertyFieldInfo fieldInfo = new PropertyFieldInfo(type, name);
            
            UpdateFieldInfo(fieldInfo);
            Bind(fieldInfo, subview);
        }
        protected override void AddNormalNodeField()
        {
            bool isEnum = node.NodeModel.Header.AccessKeyword == AccessKeyword.Enum;
            
            PropertySectionSubView subview = view.CreateNormalNodeSubview(
                "NonModified",
                $"{(isEnum ? $"Name_{subviewItemBinders.Count}" : "object")}",
                $"{(isEnum ? $"{subviewItemBinders.Count}" : "Property")}"
            );
            subviewItems.Add(subview);
            subview.declarationTypeRepeatButton.SetDisplay(!isEnum);
            // bind to Model
            PropertyFieldInfo fieldInfo = isEnum
                ? new PropertyFieldInfo(subview.typeTextField.text, subviewItemBinders.Count.ToString()) // enum : name - value 
                : new PropertyFieldInfo(subview.typeTextField.text, $"{subview.nameTextField.text}_{subviewItemBinders.Count}"); // normal : type - name
            
            UpdateFieldInfo(fieldInfo);
            Bind(fieldInfo, subview);
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }
        protected override void AddEventNodeField(PropertyFieldInfo fieldInfo)
        {
            PropertySectionSubView subview = view.CreateEventNodeSubview("object");
            subviewItems.Add(subview);

            UpdateFieldInfo(fieldInfo);
            Bind(fieldInfo, subview);
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }
        protected override void LoadNormalNodeField(PropertyFieldInfo fieldInfo)
        {
            bool isEnum = node.NodeModel.Header.AccessKeyword == AccessKeyword.Enum;
            
            PropertySectionSubView subview = view.CreateNormalNodeSubview(
                "NonModified",
                $"{(isEnum ? $"Name_{subviewItemBinders.Count}" : fieldInfo.Type)}",
                $"{(isEnum ? $"{subviewItemBinders.Count}" : fieldInfo.Name)}"
            );
            subviewItems.Add(subview);

            subview.declarationTypeRepeatButton.SetDisplay(!isEnum);
            
            UpdateFieldInfo(fieldInfo);
            Bind(fieldInfo, subview);
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }

        protected override void LoadEventNodeField(PropertyFieldInfo fieldInfo)
        {
            PropertySectionSubView subview = view.CreateEventNodeSubview(fieldInfo.Type);
            subviewItems.Add(subview);

            UpdateFieldInfo(fieldInfo);
            Bind(fieldInfo, subview);
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }
        protected override void RemoveSubviewItem(PropertySectionSubView subviewToRemove)
        {
            if (!subviewItems.Any(subviewToRemove.IsMatch)) return;
            subviewToRemove.group.RemoveFromHierarchy();
            subviewItems.Remove(subviewToRemove);
        }
        
        protected override int GetSubviewItemCount(PropertySectionSubView subview)
        {
            int count = subviewItems.Count(subview.IsMatch);
            Debug.Log($"Count : {count}");
            return count;
        }
        
        protected override void RemoveSubviewElements()
        {
            if(subviewItems.Count == 0) return;
            subviewItems.ForEach(subView => subView.RemoveFromHierarchy());
        }
    }
}