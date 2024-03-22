public interface IMessenger
{
    void SetUpSubscribe();
    void ReleaseSubscribe();
    void Publish();
}