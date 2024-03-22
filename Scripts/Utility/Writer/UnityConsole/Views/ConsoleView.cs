using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace Writer.CommandConsole
{
    public class ConsoleView : UxmlItem<VisualElement>
    {
        public readonly UxmlItem<VisualElement> mainContainer;
        public readonly UxmlItem<VisualElement> contentContainer;
        
        public readonly UxmlItem<TextField> contentInputField;
        public readonly UxmlItem<ListView> autoCompleteView;
        
        private readonly UxmlItem<VisualElement> inputContainer;
        private readonly UxmlItem<ScrollView> contentScrollView;
        public ConsoleView()
        {
            mainContainer = new VisualElement().SetUp();
            contentContainer = new VisualElement().SetUp();
            inputContainer = new VisualElement().SetUp();
            
            contentScrollView = new ScrollView().SetUp();
            contentInputField = new TextField().SetUp();
            autoCompleteView = new ListView().SetUp();
            
            
            mainContainer
                .StyleFlexGrow(1)
                .StyleFlexShrink(1)
                .StyleFlexDirection(FlexDirection.Row);

            contentContainer
                .StyleFlexGrow(1)
                .StyleFlexShrink(1)
                .StyleFlexDirection(FlexDirection.Column);
            
            inputContainer
                .StyleFlexGrow(1)
                .StyleFlexShrink(1)
                .StyleFlexDirection(FlexDirection.Column);
            
            contentScrollView
                .SetName("content-scroll-view")
                .StyleFlexGrow(1)
                .StyleFlexShrink(1);

            contentInputField
                .SetName("content-input-field")
                .StyleFlexGrow(1)
                .StyleFlexShrink(1);
            
            autoCompleteView
                .SetName("auto-complete-view")
                .StyleFlexGrow(1)
                .StyleFlexShrink(1);
            
            mainContainer.Add(contentContainer);
            contentContainer.Add(contentScrollView, inputContainer);
            inputContainer.Add(contentInputField, autoCompleteView);
        }
        
        public void SetAutoCompleteView(params string[] autoComplete)
        {
            autoCompleteView.element.itemsSource = autoComplete;
            autoCompleteView.element.MarkDirtyRepaint();
        }

        public void SendToLog(CommandLineElement message)
        {
            contentScrollView.element.contentContainer.Add(message.element);
        }
    }
}