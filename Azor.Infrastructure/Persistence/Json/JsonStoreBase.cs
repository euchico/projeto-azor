using System.Text.Json;
using Azor.Infrastructure.Persistence.Abstractions;

namespace Azor.Infrastructure.Persistence.Json;

public abstract class JsonStoreBase<T> : IJsonStore<T>
{
    private readonly string _fullPath;
    protected abstract string FolderPath { get; }

    protected virtual JsonSerializerOptions JsonOptions => new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    protected JsonStoreBase()
    {
        _fullPath = Path.Combine(GetSolutionRootPath(), FolderPath);
        Directory.CreateDirectory(_fullPath);
    }

    private static string GetSolutionRootPath()
    {
        var currentDirectory = new DirectoryInfo(AppContext.BaseDirectory);

        while (currentDirectory != null)
        {
            if (currentDirectory.GetFiles("*.sln").Any())
            {
                return currentDirectory.FullName;
            }

            currentDirectory = currentDirectory.Parent;
        }

        return AppContext.BaseDirectory;
    }

    public void Save(string code, T entity)
    {
        var path = Path.Combine(_fullPath, $"{code}.json");
        var json = JsonSerializer.Serialize(entity, JsonOptions);
        File.WriteAllText(path, json);
    }

    public T? Load(string code)
    {
        var path = Path.Combine(_fullPath, $"{code}.json");

        if (!File.Exists(path))
            return default;

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json, JsonOptions);
    }

    public IEnumerable<T> LoadAll()
    {
        var result = new List<T>();

        foreach (var file in Directory.GetFiles(_fullPath, "*.json"))
        {
            var json = File.ReadAllText(file);
            var entity = JsonSerializer.Deserialize<T>(json, JsonOptions);

            if (entity != null)
                result.Add(entity);
        }

        return result;
    }
}
