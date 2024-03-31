public class DDDGeneratorSettings : WriterSettingBase
{
    private string DDDRootPath => "Data Driven Design/Domain/Data";
    public string EntityRootPath => $"{scriptRootPath}/{DDDRootPath}/Model";
    public string OrderSORootPath => $"{scriptRootPath}/{DDDRootPath}/[Except] Order";
    public string OrderSOEditorRootPath => $"{scriptRootPath}/{DDDRootPath}/Order/Editor";
    public string RepositorySORootPath => $"{scriptRootPath}/{DDDRootPath}/[Except] Repository";
    public string EnumScriptPath => $"{scriptRootPath}/{DDDRootPath}/Enum";
    public string EnumJsonConverterPath => $"{scriptRootPath}/{DDDRootPath}/CustomJsonConverter/EnumConverter";
    public string DatabasePath => $"{scriptRootPath}/{DDDRootPath}/Resources/Documents";
    public string DestinationPath => $"{scriptRootPath}/{DDDRootPath}/Resources/DatabaseSO";
}