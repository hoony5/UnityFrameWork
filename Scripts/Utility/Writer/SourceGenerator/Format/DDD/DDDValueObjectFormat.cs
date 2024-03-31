namespace Writer.SourceGenerator.Format
{
    public class DDDValueObjectFormat : FrameFormatBase
    {
        public DDDValueObjectFormat(
            string nameSpace,
            string usingSpace) 
            : base(nameSpace, usingSpace)
        {
        }

        /// <summary>
        /// {0} : AccessType
        /// {1} : Name
        /// {2} : Inheritances
        /// {3} : Properties
        /// {4} : constructorParameters
        /// {5} : constructorInheritances
        /// {6} : constructor
        /// {7} : base.log
        /// {8} : log
        /// </summary>
        /// <param name="usingSpace"></param>
        /// <returns></returns>
        protected override string GetFormatWithoutNamespace(string usingSpace)
        {
            return $@"{usingSpace}
[System.Serializable]
{{0}} {{1}} {{2}}
{{
{{3}}
      
    public {{1}}() {{ }}

    [JsonConstructor]
    public {{1}}({{4}})
        {{5}}
    {{
{{6}}
    }}
    
    public override string ToString()
    {{
#if UNITY_EDITOR
        return $@""
{{7}}
[{{1}}]
{{8}}
"";
#else
        return base.ToString();
#endif
    }}
}}";
        }
    }
}