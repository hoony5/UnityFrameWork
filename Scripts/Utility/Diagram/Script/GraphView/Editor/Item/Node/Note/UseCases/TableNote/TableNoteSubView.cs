using System.Linq;
using AYellowpaper.SerializedCollections;
using Share;
using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

//todo :: reduce nested if statements
namespace Diagram
{
    public class TableNoteSubView
    {
        private NoteSectionView mainView;
        private VisualElement tableContainer;
        private VisualElement tableLeftContainer;
        private VisualElement cellContainer;
        public Button tableAddColumnButton;
        private VisualElement tableRightContainer;
        public CellContainer columnButtonContainer;
        public Button tableAddRowButton; // with empty element left side
        // swap
        private VisualElement swapContainer;
        public Button swapTargetButton; // row or column
        public Button swapButton; // swap button 
        public TextField swapFromTextField;
        public TextField swapToTextField;
        // options
        private VisualElement optionContainer;
        // safe Clear
        public Toggle safeClearToggle;
        public Label safeClearLabel;
        // visibility 
        public Toggle visibilityButtonToggle;
        public Label visibilityButtonToggleLabel;
        public VisualElement CellContainer => cellContainer;
        public SerializedDictionary<int, CellContainer> rows;
        
        public TableNoteSubView(NoteSectionView mainView)
        {
            this.mainView = mainView;
            rows = new SerializedDictionary<int, CellContainer>();
            cellContainer = new VisualElement();
            mainView.CreatePlain("Table");
            CreateSwapView();
            CreateOptionsToggle();
            mainView.plain.CreateVerticalSpace(10);
            CreateTableNote();    
            mainView.plain.CreateVerticalSpace(50);
        }
        private void CreateTableNote()
        {
            tableContainer = new VisualElement();
            tableContainer.Add(CreateColumnControlView());
            tableContainer.Add(CreateRowControlView());
            
            tableContainer.SetFlexDirection(FlexDirection.Row);
            tableContainer.SetBorderLeftWidth(20);
            // done
            mainView.plain.Add(tableContainer);
        }

        private VisualElement CreateColumnControlView()
        {
            tableLeftContainer = new VisualElement();
            VisualElement topLeftEmpty = CreateSpace(30, 20);
            topLeftEmpty.SetFlexGrow(0);
            VisualElement bottomLeftEmpty = CreateSpace(30, 20);
            
            tableAddColumnButton = CreateButton("+");
            
            tableContainer.Add(tableLeftContainer);
            
            tableLeftContainer.Add(topLeftEmpty);
            tableLeftContainer.Add(tableAddColumnButton);
            tableLeftContainer.Add(bottomLeftEmpty);
            
            tableAddColumnButton.SetWidth(30);
            tableAddColumnButton.SetFlexGrow(1);
            tableLeftContainer.SetFlexDirection(FlexDirection.Column);
            tableAddColumnButton.SetTextAlign(TextAnchor.MiddleCenter);
            return tableLeftContainer;
        }

        private VisualElement CreateRowControlView()
        {
            tableRightContainer = new VisualElement();
            tableAddRowButton = CreateButton("+");
            VisualElement buttonContainer = new VisualElement();
            VisualElement topRightEmpty = new VisualElement();
            topRightEmpty.SetSize(45, 20);
            
            tableContainer.Add(tableRightContainer);
            
            buttonContainer.Add(tableAddRowButton);
            buttonContainer.Add(topRightEmpty);
            
            tableRightContainer.Add(buttonContainer);
            tableRightContainer.Add(cellContainer);
            
            topRightEmpty.SetFlexGrow(0);
            buttonContainer.SetFlexDirection(FlexDirection.Row);
            tableAddRowButton.SetHeight(20);
            tableAddRowButton.SetFlexGrow(1);
            tableRightContainer.SetFlexDirection(FlexDirection.Column);
            tableAddRowButton.SetTextAlign(TextAnchor.MiddleCenter);
            
            return tableRightContainer;
        }

        private void CreateSwapView()
        {
            swapContainer = new VisualElement();
            
            swapButton = CreateButton("Swap");
            swapTargetButton = CreateButton("Row");
            swapFromTextField = CreateTextField(string.Empty);
            swapToTextField = CreateTextField(string.Empty);
            Label label = CreateLabel("To");
            
            swapContainer.Add(swapButton);
            swapContainer.Add(swapTargetButton);
            swapContainer.Add(swapFromTextField);
            swapContainer.Add(label);
            swapContainer.Add(swapToTextField);
            
            swapButton.SetSize(40, 20);
            swapButton.SetFlexGrow(0);
            
            swapTargetButton.SetSize(40, 20);
            swapTargetButton.SetFlexGrow(0);
            
            swapFromTextField.SetSize(40, 20);
            swapFromTextField.SetFlexGrow(0);
            
            label.SetSize(20, 20);
            label.SetFlexGrow(0);
            label.SetTextAlign(TextAnchor.LowerCenter);
            
            swapToTextField.SetSize(40, 20);
            swapToTextField.SetFlexGrow(0);
            
            swapContainer.SetFlexDirection(FlexDirection.Row);
            swapContainer.SetHeight(30);
             
            mainView.plain.Add(swapContainer);
        }

        private void CreateOptionsToggle()
        {
            optionContainer = new VisualElement();
            VisualElement safeSaveToggleContainer = new VisualElement();
            VisualElement visibilityToggleContainer = new VisualElement();
            
            safeClearToggle = CreateToggle(true);
            safeClearLabel = CreateLabel("Safe Clear");
            
            visibilityButtonToggleLabel = CreateLabel("Visibility Buttons");
            visibilityButtonToggle = CreateToggle(true);
            
            safeSaveToggleContainer.Add(safeClearToggle);
            safeSaveToggleContainer.Add(safeClearLabel);
            
            visibilityToggleContainer.Add(visibilityButtonToggle);
            visibilityToggleContainer.Add(visibilityButtonToggleLabel);
            
            optionContainer.Add(safeSaveToggleContainer);
            optionContainer.Add(visibilityToggleContainer);
            
            mainView.plain.Add(optionContainer);
            
            optionContainer.SetFlexDirection(FlexDirection.Column);
            optionContainer.style.borderBottomColor = Color.red;
            optionContainer.SetBorderBottomWidth(1);
            
            safeSaveToggleContainer.SetFlexDirection(FlexDirection.Row);
            visibilityToggleContainer.SetFlexDirection(FlexDirection.Row);

            safeClearToggle.SetFlexGrow(0);
            safeClearToggle.SetSize(20, 20);
            safeClearLabel.SetFlexGrow(0);
            safeClearLabel.SetTextAlign(TextAnchor.MiddleCenter);
            safeClearLabel.SetFontSize(12);
            safeClearLabel.SetSize(60, 20);
            
            visibilityButtonToggle.SetFlexGrow(0);
            visibilityButtonToggle.SetSize(20, 20);
            visibilityButtonToggleLabel.SetFlexGrow(0);
            visibilityButtonToggleLabel.SetTextAlign(TextAnchor.MiddleCenter);
            visibilityButtonToggleLabel.SetFontSize(12);
            visibilityButtonToggleLabel.SetSize(100, 20);
        }

        public void CreateColumnDeletionButtons(TableNote note)
        {
            columnButtonContainer = new CellContainer(note);
            columnButtonContainer.view.CreateColumnDeletionButton(note.view.rows.Values.First().ColumnCount);
            cellContainer.Add(columnButtonContainer);
        }

        public CellContainer GetRow(int rowIndex)
        {
            return !rows.ContainsKey(rowIndex) ? null : rows[rowIndex];
        }

        public void Reset()
        {
            swapContainer.Clear();
            optionContainer.Clear();
            tableContainer.Clear();
            
            mainView.plain.SafeRemove(swapContainer);
            mainView.plain.SafeRemove(optionContainer);
            mainView.plain.SafeRemove(tableContainer);
        }
    }

}