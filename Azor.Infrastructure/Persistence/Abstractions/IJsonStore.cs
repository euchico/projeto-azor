namespace Azor.Infrastructure.Persistence.Abstractions;

public interface IJsonStore<T>
{
    void Save(string code, T entity);
    T? Load(string code);
    IEnumerable<T> LoadAll();
}