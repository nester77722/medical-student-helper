using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.LocalData.Services;
public interface ILanguageLocalService
{
    Task<string> GetLanguageAsync();
    Task SaveLanguageAsync(string cultureName);
}
