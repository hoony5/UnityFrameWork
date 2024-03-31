using System;

namespace Diagram
{
    public interface ISubNote : IDisposable
    {
        void Load(DiagramNodeModel nodeModel);
        void Clear();

        string GetDescription(ExportFileType exportFileType);
    }
}