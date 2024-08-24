using MedicalStudentHelper.UserData.Models.CreateModels;
using MedicalStudentHelper.UserData.Models.GetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.UserData.Services.Interfaces;
public interface IUserService
{
    Task<GetUserModel> CreateUserFromGoogleAccountAsync(CreateUserFromGoogleAccountModel createModel);
    Task<GetUserModel> GetUserByGoogleIdAsync(string googleId);
    Task<GetUserModel> GetUserByIdAsync(string id);
}
