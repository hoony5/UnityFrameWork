using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    namespace UIElements
    {
        public class UIBinderFormat : IWriterFormat
        {
            /// <summary>
            /// {0} : Binder Name
            /// {1} : Model Name
            /// {2} : Event Register - add
            /// {3} : Event Register - remove
            /// {4} : Event Callbacks
            /// </summary>
            /// <returns></returns>
            public string GetFormat()
            {
                string content = $@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof({{1}}))]
public class {{0}} : MonoBehaviour
{{
    [SerializeField] public {{1}} {{0}} {{get; set;}}

    // TODO :: Add your Event here   
    private void OnEnable()
    {{
        StartCoroutine(Init());    
    }}        
                
    private void OnDisable()
    {{ 
        StopAllCoroutines();
        ReleaseEvents();
    }}
                
    // Note :: There is a UnityEngine's bug of rootVisualElement initialization.
    //        So, I used Coroutine to wait for rootVisualElement initialization.
    private IEnumerator Init()
    {{
        while({{{0}}} is null)
            yield return null;

        while({{{0}}}.uiDocument.rootVisualElement is null || !{{{0}}}.InitSuccess)
            yield return null;

        // element's events
{{2}}
    }}
    private void ReleaseEvents()
    {{
{{3}}
    }}
                
    // TODO :: Implement your event callback here 
{{4}}
}}
";
                return content;
            }
        }
    }

}