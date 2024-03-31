using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Writer.SourceGenerator
{
    public class UISourceWriter : SourceWriterBase
    {
        protected List<(string type, string name)> _elementInfos = new List<(string type, string name)>(128);
        protected StringBuilder _classFrameBuilder = new StringBuilder();
        protected StringBuilder _classFieldBuilder = new StringBuilder();
        protected void GetVisualElementNames(string uxmlPath, string ignorePrefix = "___")
        {
            _elementInfos.Clear();
            XDocument document = XDocument.Load(uxmlPath);
            IEnumerable<(string type, string name)> elements = document.Elements().Descendants()
                .Select(i => (i.Name.LocalName, i.Attribute("name")?.Value));

            foreach ((string type, string name) element in elements)
            {
                // ignore empty name element
                if (string.IsNullOrEmpty(element.name)) continue;
                // ignore case
                if (element.name.Contains(ignorePrefix)) continue;

                _elementInfos.Add(element);
            }
        }

        protected string GenerateInitModelQuery()
        {
            _classFieldBuilder.Clear();

            // visualElement Query
            for (int i = 0; i < _elementInfos.Count; i++)
            {
                (string type, string name) currentElement = _elementInfos[i];
                string format =
                    $"{currentElement.name} = rootVisualElement.Q<{currentElement.type}>({currentElement.name}Name);";
                _classFieldBuilder.Append(i == _elementInfos.Count - 1 ? format : $"{format}\n\t\t");
            }

            return _classFieldBuilder.ToString();
        }

    }
}