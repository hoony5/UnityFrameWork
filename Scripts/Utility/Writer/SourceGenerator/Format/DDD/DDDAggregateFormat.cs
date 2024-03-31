namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : AccessModifier + ReturnType
    /// {1} : Name
    /// {2} : inheritances
    /// {3} : properties
    /// {4} : constructor parameters
    /// {5} : constructor
    /// {6} : ToString
    /// </summary>
    public class DDDAggregateFormat : FrameFormatBase
    {
        public DDDAggregateFormat(
            string nameSpace,
            string usingSpace) 
            : base(nameSpace, usingSpace)
        {
        }
        protected override string GetFormatWithoutNamespace(string usingSpace)
        {
            return $@"{usingSpace}
[Serializable]
{{0}} {{1}} {{2}}
{{
{{3}}
      
    public {{1}}() {{ }}

    [JsonConstructor]
    public {{1}}({{4}})
    {{
{{5}}
    }}

  public override string ToString()
    {{
#if UNITY_EDITOR
        return $@""
{{6}}
"";
#else
    return base.ToString();
#endif
    }}
}}";
        }
    }
}