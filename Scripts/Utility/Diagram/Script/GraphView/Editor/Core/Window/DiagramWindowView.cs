using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class DiagramWindowView
    {
        private DiagramWindow window;
        public Toolbar toolbar;
        public Button blackboardButton;
        public Button gptButton;
        public Button databaseButton;
        public TextField titleTextField;
        public Button saveButton;
        public Button saveAsButton;
        public Button loadButton;
        public Button generateButton;
        public Button minimapButton;
        public Button resetButton;
        public Button clearButton;
        public Button testRegisterButton;
        public Button testButton;
        
        public DiagramWindowView(DiagramWindow window)
        {
            this.window = window;
        }
        public void Create()
        {
            toolbar = new Toolbar();

            Label titleLabel = new Label("Title : ");
            titleTextField = CreateTextField("new Diagram", 200);
            VisualElement buttonContainer = new VisualElement();
            VisualElement widthSpace = new VisualElement();
            
            blackboardButton = CreateButton("Blackboard");
            gptButton = CreateButton("GPT");
            databaseButton = CreateButton("Database");
            
            saveButton = CreateButton("Save");
            saveAsButton = CreateButton("SaveAs");
            loadButton = CreateButton("Load");
            generateButton = CreateButton("Generate scripts");
            minimapButton = CreateButton("Minimap");
            resetButton = CreateButton("Reset");
            clearButton = CreateButton("Clear");
            testButton = CreateButton("Test");
            testRegisterButton = CreateButton("Test Register");

            toolbar.Add(titleLabel);
            toolbar.Add(titleTextField);
            toolbar.Add(blackboardButton);
            toolbar.Add(gptButton);
            toolbar.Add(databaseButton);
            toolbar.Add(widthSpace);
            
            buttonContainer.Add(saveButton);
            buttonContainer.Add(saveAsButton);
            buttonContainer.Add(loadButton);
            buttonContainer.Add(generateButton);
            buttonContainer.Add(minimapButton);
            buttonContainer.Add(resetButton);
            buttonContainer.Add(clearButton);
            buttonContainer.Add(testRegisterButton);
            buttonContainer.Add(testButton);
            toolbar.Add(buttonContainer);

            blackboardButton.SetFlexGrow(0);
            blackboardButton.SetHeight(20);
            blackboardButton.SetWidth(100);
            gptButton.SetFlexGrow(0);
            gptButton.SetHeight(20);
            gptButton.SetWidth(100);
            databaseButton.SetFlexGrow(0);
            databaseButton.SetHeight(20);
            databaseButton.SetWidth(100);
            toolbar.SetHeight(25);
            titleLabel.SetFontSize(12);
            titleLabel.SetTextAlign(TextAnchor.MiddleCenter);
            titleTextField.SetFlexGrow(0);
            widthSpace.SetFlexGrow(1);
            buttonContainer.SetFlexDirection(FlexDirection.Row);
            
            window.rootVisualElement.Add(toolbar);
        }
    }

}