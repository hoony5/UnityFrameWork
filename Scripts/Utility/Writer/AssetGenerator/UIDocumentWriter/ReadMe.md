## UIDocument Writer

### Overview
- UIDocumentWriter is used to create a new Uxml and Uss file for each UIElementView.

### Why
- The purpose of this tool is to make it easier to create new UIElementView files. 
  - Continuously Creation and Modification workflow.
    - Now you can create or modify ui layout and style on the script.
  - Build Pattern
    - Inline Style too complex to write and read. so, you can use the build pattern to create a new UIElementView.
  - Export Uxml File or Uss File
  - Generate Model Style Code
    - You can generate a model style code from the UIElementView.
      
      ```csharp
        using UIElements;
        using UnityEngine.UIElements;
        
        public class GridCell : UxmlItem<VisuaElement>
        {
            public readonly UxmlItem<VisualElement> headContainer;
            public readonly UxmlItem<VisualElement> imageContainer;
            public readonly UxmlItem<VisualElement> labelContainer;
        
            public readonly UxmlItem<Label> headLine;
            public readonly UxmlItem<Label> footerLine;
         
            public GridCell()
            {
               headContainer = new UxmlItem<VisualElement>();
               imageContainer = new UxmlItem<VisualElement>();
               labelContainer = new UxmlItem<VisualElement>();
        
               headLine = new Label().SetUp();
               footerLine = new Label().SetUp();
        
                // layout
               headContainer.Add(headLine);
               labelContainer.Add(footerLine);
                
               this.Add(headContainer);
               this.Add(imageContainer);
               this.Add(labelContainer);
        
               // style
               headContainer
                            .StyleFlexGrow(1)
                            .StyleFlexShrink(1)
                            .StyleFlexDirection(FlexDirection.Row);
                
               imageContainer
                            .StyleFlexGrow(1)
                            .StyleFlexShrink(1)
                            .StyleFlexDirection(FlexDirection.Row);
        
               labelContainer
                            .StyleFlexGrow(1)
                            .StyleFlexShrink(1)
                            .StyleFlexDirection(FlexDirection.Row);
        
               headLine
                            .StyleFlexGrow(1)
                            .StyleFlexShrink(1)
                            .StyleFlexDirection(FlexDirection.Row);
        
               footerLine
                            .StyleFlexGrow(1)
                            .StyleFlexShrink(1)
                            .StyleFlexDirection(FlexDirection.Row);
            }
        
            public void Export()
            {
                // or USS ( only contains attribute settings )
                string uxmlWithStyle = this.ToUXmlLines(StyleMode.Inline); 
                string ussBlock = this.ExtractUssBlock("ussBlock");         // string ussBlock = rootDetail.ExtractUssBlock("ussBlock", selector);
        
                // base root = "Assets/"
                UxmlGenerator.ExportUxml("Generated/Uxml/Grid.uxml", uxmlWithStyle);
                UssGenerator.ExportUss("Generated/Uss/Grid.uss", ussBlock);
        
                FileExporter.RefreshEditor();
            }
        }

        ```

