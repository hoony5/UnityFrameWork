using System;

namespace Diagram
{
    public interface ISectionFactory : IDisposable
    {
        void Setup();
        void Load();
        void Reload();
    }
}