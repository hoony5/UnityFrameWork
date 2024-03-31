using UnityEngine;
using UnityEngine.Serialization;

public class DocumentGeneratorConfig : ScriptableObject
{
    public string creatorPath;
    public string markDownConfig;
    public string converterPath;
    public string converterConfigPath;
    public string publisherPath;
    public string publisherConfigPath;
}