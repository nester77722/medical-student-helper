using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.WPF.Services.Interfaces;
public interface IAppStateService
{
    bool IsUserLoggedIn { get; }
    string CurrentUserId { get; }

    event EventHandler StateChanged;
    void DeleteLoginedUser();
    void SaveLoginedUser(string userId);
}
