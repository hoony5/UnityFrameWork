using Unity.Properties;
using UnityEngine.UIElements;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : AccessModifier
    /// {1} : Name
    /// {2} : properties
    /// </summary>
    public class DDDOrderSOFormat : FrameFormatBase
    {
        public DDDOrderSOFormat(
            string nameSpace,
            string usingSpace)
            : base(nameSpace, usingSpace)
        {
            
        }

        protected override string GetFormatWithoutNamespace(string usingSpace)
        {
            return $@"{usingSpace}
[Serializable]
{{0}} class {{1}}Order : AggregateOrderSOBase<{{1}}Repository>
{{
    [field:SerializeField] private AggregateOrder<{{1}}> Order {{ get; set; }}

    public override void CompositeRepository()
    {{
#if UNITY_EDITOR
        base.OnValidate();
        if(Ignore) 
        {{
            Debug.Log("" {{1}}Order is Ignored "");
            return;
        }}
        if (!Order.IsFilledConstructDataArray())
        {{
            Debug.LogError(""Not filled ConstructDataArray"");
            return;
        }}
        
        if(RepositorySO is null) throw new NullReferenceException(""RepositorySO is null"");
        RepositorySO.Combined(Order.RequestCreateRepository());
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }}
}}
";
        }
    }
}