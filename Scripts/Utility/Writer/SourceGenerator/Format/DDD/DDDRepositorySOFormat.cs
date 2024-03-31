namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : AccessType
    /// {1} : Name
    /// </summary>
    public class DDDRepositorySOFormat : FrameFormatBase
    {
        public DDDRepositorySOFormat(
            string nameSpace,
            string usingSpace)
            : base(nameSpace, usingSpace)
        {
        }

        protected override string GetFormatWithoutNamespace(string usingSpace)
        {
            return $@"{usingSpace}
[System.Serializable]
{{0}} sealed class {{1}}Repository : RepositorySOBase
{{
    [field:SerializeField] private Repository<{{1}}> Map {{ get; set; }}

    public void Combined(IRepository<{{1}}> map)
    {{
#if UNITY_EDITOR
        Map = map as Repository<{{1}}>;        
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }}
}}
";
        }
    }
}