using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace Writer.AssetGenerator.UIElement.Test
{
    public class UxmlWriterTest : MonoBehaviour
    {
        public UIDocument UIDocument;
        
        string uxmlWithStylePath = "Services/File/Generated/SampleWithStyle.uxml";
        string uxmlWithoutStylePath = "Services/File/Generated/SampleWithoutStyle.uxml";

        [TextArea] public string test;
        [Button]
        private void OverwriteVisualAsset()
        {
#if UNITY_EDITOR
            var visualElement = UnityEditor.AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(Path.Combine("Assets", uxmlWithStylePath));
            UIDocument.visualTreeAsset = visualElement;
#endif
        }

        private void SetUpTextStreamer()
        {
            TextStreamer streamer = FileExporter.GetTextStreamer("Assets/Test/Generated", "uss", "txt");
        }

        private void StreamWriteText(TextStreamer streamer, string input)
        {
            // trigger : if (Input.GetKeyDown(KeyCode.Return))
                streamer?.Write(input);
        }
        [Button]
        private void ConvertText()
        {
            VisualElement widthSpace = new VisualElement();
            UxmlItem<VisualElement> widthSpaceDetail =
                widthSpace
                    .SetUp()
                    .StyleFlexGrow(0)
                    .StyleWidth(10)
                    .StyleHeight(100, LengthUnit.Percent)
                    .StyleBackgroundColor(new Color(0.1f, 0.1f, 0.1f, 1));
            
            Label label = new Label();
            UxmlItem<Label> labelDetail =
                label
                    .SetUp()
                    .StyleFlexGrow(0)
                    .StyleWidth(100)
                    .StyleFontSize(12)
                    .SetText("Hello World")
                    .StyleTextAlign(TextAnchor.MiddleLeft);
            
            TextField inputField = new TextField();
            UxmlItem<TextField> inputFieldDetail =
                inputField
                    .SetUp()
                    .StyleFlexGrow(0)
                    .StyleWidth(100)
                    .StyleFontSize(12)
                    .StyleTextAlign(TextAnchor.MiddleLeft);
            
            Button button = new Button();
            UxmlItem<Button> buttonDetail =
            button
                .SetUp()
                .StyleFlexGrow(0)
                .StyleWidth(100)
                .StyleHeight(100)
                .StyleTextAlign(TextAnchor.MiddleCenter)
                .SetText("Click Me");
            
            VisualElement child = new VisualElement();
            UxmlItem<VisualElement> childDetail =
                child
                    .SetUp()
                    .StyleFlexGrow(0)
                    .StyleWidth(300)
                    .StyleHeight(100, LengthUnit.Percent)
                    .StyleBackgroundColor(new Color(0.1f, 0.1f, 0.1f, 1))
                    .StyleFlexDirection(FlexDirection.Row)
                    .StyleJustifyContent(Justify.Center)
                    .StyleAlignItems(Align.Center)
                    .StyleAlignContent(Align.Center)
                    .StyleFlexWrap(Wrap.NoWrap)
                    .Add(widthSpaceDetail, labelDetail, inputFieldDetail, buttonDetail);

            VisualElement root = new VisualElement();
            UxmlItem<VisualElement> rootDetail =
                root
                    .SetUp()
                    .StyleFlexGrow(0)
                    .StyleWidth(300)
                    .StyleHeight(300)
                    .StyleBackgroundColor(new Color(0.1f, 0.1f, 0.1f, 1))
                    .StyleFlexDirection(FlexDirection.Row)
                    .StyleJustifyContent(Justify.Center)
                    .StyleAlignItems(Align.Center)
                    .StyleAlignContent(Align.Center)
                    .StyleFlexWrap(Wrap.NoWrap)
                    .Add(childDetail);
            
            string uxmlWithStyle = rootDetail.ToUXmlLines();
            UxmlGenerator.ExportUxml(uxmlWithStylePath, uxmlWithStyle);
            Debug.Log(uxmlWithStyle);
            
            string uxmlWithoutStyles = rootDetail.ToUXmlLines(StyleMode.Uss);
            UxmlGenerator.ExportUxml(uxmlWithoutStylePath, uxmlWithoutStyles);
            Debug.Log(uxmlWithoutStyles);
            
            string ussBlock = rootDetail.ToUssBlocks("ussBlock");
            // string ussBlock = rootDetail.ExtractUssBlock("ussBlock", selector);
            Debug.Log(ussBlock);

            FileExporter.RefreshEditor();
        }
    }
}
