using System.Collections.Generic;
using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    ///  {0} : Type
    ///  {1} : Name
    ///  {2} : Attributes
    /// 
    /// </summary>
    public class UxmlFormatStartWithChildren: IWriterFormat
    {
        public string GetFormat()
        {
            return "<ui:{0}{1}{2}>";
        }
    }
}