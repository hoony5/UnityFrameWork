namespace Writer.SourceGenerator.Format
{
    public class UIModelFrameFormat : FrameFormatBase
    {
        public UIModelFrameFormat(
            string nameSpace,
            string usingSpace = "") 
            : base(nameSpace, usingSpace)
        {
        }
        /// <summary>
        /// {0} : Model Name
        /// {1} : Fields
        /// {2} : Queries
        /// </summary>
        /// <returns></returns>
        protected override string GetFormatWithoutNamespace(string usingSpace)
        {
            return  $@"{usingSpace}
public class {{0}} : MonoBehaviour
{{
    public UIDocument uiDocument;
    private VisualElement rootVisualElement;

    // TODO :: Add your Event here
    {{1}}
	private bool _initSuccess = false;
	public bool InitSuccess => _initSuccess;

    private void OnEnable()
    {{
        StartCoroutine(Init());    
    }}        
                
    private void OnDisable()
    {{ 
        StopAllCoroutines();
    }}
                
    // Note :: There is a UnityEngine's bug of rootVisualElement initialization.
    // So, I used Coroutine to wait for rootVisualElement initialization.
    private IEnumerator Init()
    {{
        while(uiDocument is null)
            yield return null;

        while(uiDocument.rootVisualElement is null)
            yield return null;
                    
        rootVisualElement = uiDocument.rootVisualElement;

        // element's Queries
        {{2}}

		_initSuccess = true;
    }}
}}
";   
        }
    }

}