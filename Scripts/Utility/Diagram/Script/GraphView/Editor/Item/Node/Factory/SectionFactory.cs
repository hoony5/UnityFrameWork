using AYellowpaper.SerializedCollections;

namespace Diagram
{
    public class SectionFactory : ISectionFactory
    {
        private readonly SerializedDictionary<string, ISectionFactory> partFactories;

        public SectionFactory()
        {
            partFactories = new SerializedDictionary<string, ISectionFactory>();
        }
        public void AddSection(ISectionFactory factory)
        {
            partFactories.Add(factory.GetType().Name, factory);
        }

        public bool TryGetSection<TType>(out TType result) where TType : ISectionFactory
        {
            result = default;
            bool exist = partFactories.TryGetValue(typeof(TType).Name, out ISectionFactory factory);
            if (exist)
                result = (TType)factory;
            return exist;
        }

        public void Setup()
        {
            foreach (ISectionFactory part in partFactories.Values)
            {
                part.Setup();
            }
        }

        public void Load()
        {
            foreach (ISectionFactory part in partFactories.Values)
            {
                part.Load();
            }
        }

        public void Reload()
        {
            foreach (ISectionFactory part in partFactories.Values)
            {
                part.Reload();
            }
        }

        public void Dispose()
        {
            foreach (ISectionFactory part in partFactories.Values)
            {
                part.Dispose();
            }
        }
    }
}
