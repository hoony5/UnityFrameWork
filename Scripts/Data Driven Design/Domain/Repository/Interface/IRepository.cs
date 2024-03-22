public interface IRepository<TCategory> where TCategory : ICategory
{
    void Add(TCategory entity);
    void Clear();
    bool TryGetData(string category, string objectName, out TCategory dataModel);
}