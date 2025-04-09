namespace rubber_tree_test_backend.Interfaces;

public interface IJsonDataService
{
    Task<List<T>> GetDataAsync<T>(string fileName);
    Task SaveDataAsync<T>(string fileName, List<T> data);
}
