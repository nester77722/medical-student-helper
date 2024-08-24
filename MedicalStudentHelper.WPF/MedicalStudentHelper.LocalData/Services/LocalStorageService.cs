using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalStudentHelper.LocalData.Services;
public class LocalStorageService : ILocalStorageService
{
    private readonly string _baseFolderPath;
    private readonly IEncryptionService _encryptionService;

    public LocalStorageService(IEncryptionService encryptionService)
    {
        _encryptionService = encryptionService;

        var programFolderName = "MedicalStudentHelper";
        _baseFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), programFolderName);

        if (!Directory.Exists(_baseFolderPath))
        {
            Directory.CreateDirectory(_baseFolderPath);
        }
    }

    public async Task SaveDataAsync<T>(string contextName, string fileName, T data)
    {
        string contextPath = Path.Combine(_baseFolderPath, contextName);

        if (!Directory.Exists(contextPath))
        {
            Directory.CreateDirectory(contextPath);
        }

        string filePath = Path.Combine(contextPath, fileName);
        string jsonData = JsonSerializer.Serialize(data);

        // Шифрование данных перед сохранением
        var encryptedData = _encryptionService.Encrypt(jsonData);

        await File.WriteAllTextAsync(filePath, encryptedData);
    }

    public async Task<T> LoadDataAsync<T>(string contextName, string fileName)
    {
        string filePath = Path.Combine(_baseFolderPath, contextName, fileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified file does not exist.", fileName);

        // Чтение и дешифрование данных
        var encryptedData = await File.ReadAllTextAsync(filePath);
        string jsonData = _encryptionService.Decrypt(encryptedData);

        return JsonSerializer.Deserialize<T>(jsonData);
    }

    public void DeleteData(string contextName, string fileName)
    {
        string filePath = Path.Combine(_baseFolderPath, contextName, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public bool FileExists(string contextName, string fileName)
    {
        string filePath = Path.Combine(_baseFolderPath, contextName, fileName);
        return File.Exists(filePath);
    }
}
