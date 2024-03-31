using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class DiagramMemoView
    {
        public readonly Button fontColorButton;
        public readonly Button noteColorButton;
        
        public readonly Label title;
        public readonly Label contents;

        private Color originalColor;
            
        public DiagramMemoView(DiagramMemo note)
        {
            VisualElement topContainer = new VisualElement();
            title = note.contentContainer.Q<Label>("title");
            contents = note.contentContainer.Q<Label>("contents");
            
            noteColorButton = CreateButton("C"); 
            noteColorButton.userData = "Yellow";
            
            title.SetFontColor(Color.black);
            contents.SetFontColor(Color.black);

            noteColorButton.SetFlexGrow(1);
            noteColorButton.SetFlexShrink(1);
            noteColorButton.SetSize(20, 20);
            noteColorButton.SetBackgroundColor(Color.gray);
            noteColorButton.SetFontColor(Color.white);
            noteColorButton.SetBorderColor(Color.clear);
            
            fontColorButton = CreateButton("F");
            fontColorButton.userData = "Black";
            fontColorButton.SetFlexGrow(1);
            fontColorButton.SetFlexShrink(1);
            fontColorButton.SetSize(20, 20);
            fontColorButton.SetBackgroundColor(Color.gray);
            fontColorButton.SetFontColor(Color.white);
            fontColorButton.SetBorderColor(Color.clear);
            fontColorButton.SetFontColor(Color.black);
            
            topContainer.Add(noteColorButton);
            topContainer.Add(fontColorButton);
            note.contentContainer.Insert(0, topContainer);
            
            note.SetPosition(new Rect(note.NodeModel.Header.Position, new Vector2(384, 256)));
            topContainer.SetFlexDirection(FlexDirection.Row);
            topContainer.SetAlignSelf(Align.FlexEnd);
            topContainer.SetFlexGrow(0);
            topContainer.SetFlexShrink(1);
            
            originalColor = new Color(0.9882354f, 0.8431373f, 0.4313726f, 1);
            noteColorButton.SetFontColor(originalColor);
            note.elementTypeColor = originalColor;
            note.contentContainer.SetBackgroundColor(originalColor);
            note.contentContainer.SetFontColor(Color.black);
            
            note.contents = "Description";
        }

        public Color GetSwitchFontColor(string input)
        {
            return input switch
            {
                "Black" => Color.white,
                "White" => Color.black,
                _ => Color.white
            };
        }

        public string GetSwitchFontColorText(string input)
        {
            return input switch
            {
                "Black" => "White",
                "White" => "Black",
                _ => "White"
            };
        }
        public string GetNextNoteColorText(string input)
        {
            return input switch
            {
                "Red" => "Yellow",
                "Yellow" => "White",
                "White" => "Orange",
                "Orange" => "Green",
                "Green" => "Cyan",
                "Cyan" => "Red",
                _ => "White"
            };
        }
        public Color GetNextNoteColor(string input)
        {
            return input switch
            {
                "Red" => Color.red,
                "Yellow" => originalColor,
                "White" => Color.white,
                "Orange" => new Color(1, 0.5f, 0),
                "Green" => Color.green,
                "Cyan" => Color.cyan,
                _ => originalColor
            };
        }
    }

}