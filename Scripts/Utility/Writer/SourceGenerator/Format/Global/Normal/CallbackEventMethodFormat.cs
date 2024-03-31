namespace Writer.SourceGenerator.Format
{
    public class CallbackEventMethodFormat : MethodFormat
    {
        /// <summary>
        ///  {0} : Note
        ///  {1} : return type
        ///  {2} : Name
        ///  {3} : Type
        /// </summary>
        /// <returns></returns>
        public override string GetFormat()
        {
            return @"//{0}
    private {1} {2}({3})
    {
        throw new System.NotImplementedException();
    }";
        }
    }
}