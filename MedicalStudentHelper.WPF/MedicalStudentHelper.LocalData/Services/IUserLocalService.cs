using MedicalStudentHelper.LocalData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.LocalData.Services;
public interface IUserLocalService
{
    void DeleteUser();
    Task<LocalUser> GetUserAsync();
    Task SaveUserAsync(LocalUser user);
    Task<bool> IsUserLoggedInAsync();
}
