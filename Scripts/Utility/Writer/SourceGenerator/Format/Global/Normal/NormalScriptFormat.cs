namespace Writer.SourceGenerator.Format.Writer
{
    /// <summary>
    ///{0} : access
    ///{1} : declaration
    ///{2} : type
    ///{3} : inheritances
    ///{4} : properties
    ///{5} : parameters
    ///{6} : constructors
    ///{7} : methods
    ///{8} : subscribe
    ///{9} : unsubscribe
    /// {10} : publish
    /// {11} : callbacks
    /// {12} : toString
    /// </summary>
    public class NormalScriptFormat : FrameFormatBase
    {
        public NormalScriptFormat(string nameSpace, string usingSpace) : base(nameSpace, usingSpace)
        {
            
        }

        protected override string GetFormatWithoutNamespace(string usingSpace)
        {
            return $@"{usingSpace}
[Serializable]
{{0}} {{1}} {{2}} {{3}}
{{
{{4}}

    public {{2}}({{5}})
    {{
{{6}}
    }}

{{7}}
{{8}} {{9}} {{10}} {{11}}
    public override string ToString()
    {{
{{12}}
    }}
}}
";
        }
    }
}