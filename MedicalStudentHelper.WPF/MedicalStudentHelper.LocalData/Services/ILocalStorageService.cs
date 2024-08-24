using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.LocalData.Services;
public interface ILocalStorageService
{
    Task SaveDataAsync<T>(string contextName, string fileName, T data);
    Task<T> LoadDataAsync<T>(string contextName, string fileName);
    void DeleteData(string contextName, string fileName);
    bool FileExists(string contextName, string fileName);
}
