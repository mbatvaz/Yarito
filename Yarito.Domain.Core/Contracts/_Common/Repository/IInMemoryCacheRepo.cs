namespace Yarito.Domain.Core.Contracts._Common.Repository
{
    public interface IInMemoryCacheRepo
    {
        void Set<T>(string key, T data, TimeSpan time);
        T Get<T>(string key);
        void Remove(string key);
    }
}
