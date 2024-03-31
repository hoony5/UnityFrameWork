using Share;
using UnityEngine.UIElements;

namespace Diagram
{
    public class Cell : VisualElement
    {
        public TextField TextField;
        public int column; // current index
        public int row; // parent index
        public Cell(int row, int column, string value = "")
        {
            this.column = column;
            this.row = row;
            
            TextField = VisualElementEx.CreateTextField(value.IsNullOrEmpty() ? $"cell_{row}_{column}" : value);
            TextField.SetSize(60 , 20);
            
            Add(TextField);
            
            this.SetFlexDirection(FlexDirection.Row);
            this.SetHeight(20);
        }
    }
}