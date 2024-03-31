public class ScriptGeneratorSettings : WriterSettingBase
{
    private string RootPath => "Normal";
    public string ModelRootPath => $"{scriptRootPath}/{RootPath}";
}