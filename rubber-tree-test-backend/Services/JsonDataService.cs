using rubber_tree_test_backend.Interfaces;
using System.Text.Json;


namespace rubber_tree_test_backend.Services;

public class JsonDataService(string dataDirectory = "Data") : IJsonDataService
{
    private readonly string _dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataDirectory);
    private readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public async Task<List<T>> GetDataAsync<T>(string fileName)
    {
        string? filePath = Path.Combine(_dataDirectory, fileName);

        if (!File.Exists(filePath))
        {
            return [];
        }

        string? json = await File.ReadAllTextAsync(filePath);

        return JsonSerializer.Deserialize<List<T>>(json, options) ?? [];
    }

    public async Task SaveDataAsync<T>(string fileName, List<T> data)
    {
        string? filePath = Path.Combine(_dataDirectory, fileName);
        string? json = JsonSerializer.Serialize(data, options);

        await File.WriteAllTextAsync(filePath, json);
    }
}
