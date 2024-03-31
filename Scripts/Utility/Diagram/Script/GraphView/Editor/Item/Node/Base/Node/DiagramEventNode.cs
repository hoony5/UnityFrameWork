using Share;

namespace Diagram
{
    public sealed class DiagramEventNode : DiagramNodeBase
    {
       public override SectionFactory SectionFactory { get; protected set; }
       public MethodFieldSection callbackFieldSection;
       public PropertyFieldSection messageTypeFieldSection;
        public DiagramEventNode(DiagramGUI gui, DiagramNodeModel nodeModel) 
            : base(gui, nodeModel)
        {
            name = NodeModel.Header.Name;
            NodeModel.Status.GraphElementType = GraphElementType.Event;
            NodeModel.Header.EventType = EventType.Both;
            
            // Create Factories
            SetupFactory();
            SetupContainerHierarchy();
        }
        private void SetupFactory()
        {
            SectionFactory = new SectionFactory();
            SectionFactory.AddSection(
                new PortSection(
                    this,
                    ("subscribe", "publish"),
                    ("note", "")));
            SectionFactory.AddSection(new HeaderSection(this));
            
            SectionFactory.AddSection(messageTypeFieldSection = new PropertyFieldSection(this, "Message Type"));
            SectionFactory.AddSection(callbackFieldSection = new MethodFieldSection(this, "Callbacks"));
            SectionFactory.AddSection(logSection = new LogSection(this, "Detail"));
        }
        public override void SetupContainerHierarchy()
        {
            extensionContainer.CreateVerticalSpace(10);
            extensionContainer.Add(messageTypeFieldSection.view.plain);
            extensionContainer.CreateVerticalSpace(10);
            extensionContainer.Add(callbackFieldSection.view.foldout);
            mainContainer.Add(logSection.view.foldout);
        }

        public override void Reload()
        {
            callbackFieldSection.Reload();
            messageTypeFieldSection.Reload();
            base.Reload();
        }

        public override void RepaintAllSections()
        {
            SectionFactory.Reload();
        }

        public override void Dispose()
        {
            SectionFactory.Dispose();
            base.Dispose();
        }
    }
}