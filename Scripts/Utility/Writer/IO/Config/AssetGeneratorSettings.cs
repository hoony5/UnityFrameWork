public class AssetGeneratorSettings : WriterSettingBase
{
    public string UxmlRootPath => $"{uiElementRootPath}/UXml";
    public string UssRootPath => $"{uiElementRootPath}/Uss";
    public string ModelRootPath => $"{scriptRootPath}/UIElements/Model";
    public string BinderRootPath => $"{scriptRootPath}/UIElements/Uss";
}