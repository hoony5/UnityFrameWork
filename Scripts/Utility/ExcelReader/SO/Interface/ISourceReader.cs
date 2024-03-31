using AYellowpaper.SerializedCollections;

namespace Utility.ExcelReader
{
    public interface ISourceReader
    {
        SerializedDictionary<string, ExcelSheetInfo> Database { get; }
        void Load();
    }
}