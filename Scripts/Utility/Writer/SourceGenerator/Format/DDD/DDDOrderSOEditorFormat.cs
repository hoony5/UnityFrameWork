namespace Writer.SourceGenerator.Format
{
    public class DDDOrderSOEditorFormat : FrameFormatBase
    {
        public DDDOrderSOEditorFormat(
            string nameSpace,
            string usingSpace) 
            : base(nameSpace, usingSpace)
        {
        }

        protected override string GetFormatWithoutNamespace(string usingSpace)
        {
            return $@"{usingSpace}
[CustomEditor(typeof({{0}}Order))]
public class {{0}}OrderSOEditor : Editor
{{
    private {{0}}Order _order;
     public void OnEnable()
     {{
         _order = target as {{0}}Order;
     }}

     public override void OnInspectorGUI()
     {{
         base.OnInspectorGUI();
         serializedObject.Update();
         EditorGUILayout.Space(30);
         if(GUILayout.Button(""Composite Repository"", GUILayout.Height(30)))
         {{
             _order.CompositeRepository();
         }}
         serializedObject.ApplyModifiedProperties();
         serializedObject.SetIsDifferentCacheDirty();
     }}
}}
";
        }
    }
}