using Share;

namespace Diagram
{
    public sealed class DiagramNormalNode : DiagramNodeBase
    {
        public override SectionFactory SectionFactory { get; protected set; }
        private MethodFieldSection methodFieldSection;
        public PropertyFieldSection propertyFieldSection;
        public DiagramNormalNode(DiagramGUI gui, DiagramNodeModel nodeModel) 
            : base(gui, nodeModel)
        {
            name = NodeModel.Header.Name;
            NodeModel.Status.GraphElementType = GraphElementType.Normal;
            
            // Create Factories
            SetupFactory();
            SetupContainerHierarchy();
        }
        private void SetupFactory()
        {
            SectionFactory = new SectionFactory();
            SectionFactory.AddSection(
                new PortSection(this,
                    ("parent", "child"),
                    ("subscribe", "publish"),
                    ("inject", "provide"),
                    ("note", "")));
            SectionFactory.AddSection(new HeaderSection(this));
            
            SectionFactory.AddSection(propertyFieldSection = new PropertyFieldSection(this, "Properties"));
            SectionFactory.AddSection(methodFieldSection = new MethodFieldSection(this, "Methods"));
            SectionFactory.AddSection(logSection = new LogSection(this, "Detail"));
        }

        public override void SetupContainerHierarchy()
        {
            extensionContainer.CreateVerticalSpace(10);
            extensionContainer.Add(propertyFieldSection.view.foldout);
            extensionContainer.Add(methodFieldSection.view.foldout);
            mainContainer.Add(logSection.view.foldout);
        }
        public override void Reload()
        {
            methodFieldSection.Reload();
            propertyFieldSection.Reload();
            base.Reload();
        }

        public override void RepaintAllSections()
        {
            SectionFactory.Reload();
        }
        
        public void ConvertMethodToStatic()
        {
            SectionFactory.TryGetSection(out MethodFieldSection methodSection);
            methodSection?.ConvertToStatic();
        }
        public void ConvertPropertyToStatic()
        {
            SectionFactory.TryGetSection(out PropertyFieldSection propertySection);
            propertySection?.ConvertToStatic();
        }

        public void SetVisibleEnumSection(bool isVisible)
        {
            SectionFactory.TryGetSection(out MethodFieldSection methodSection);
            SectionFactory.TryGetSection(out PropertyFieldSection propertySection);

            propertySection?.ConvertToEnumSection(isVisible);
            methodSection?.SetVisible(!isVisible);
        }
        public override void Dispose()
        {
            SectionFactory.Dispose();
            base.Dispose();
        }
    }
}
