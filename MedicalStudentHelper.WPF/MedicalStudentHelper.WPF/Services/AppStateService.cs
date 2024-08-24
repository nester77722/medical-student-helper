using MedicalStudentHelper.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.WPF.Services;
public class AppStateService : IAppStateService
{
    private bool _isUserLoggedIn;

    public bool IsUserLoggedIn
    {
        get => _isUserLoggedIn;
        private set
        {
            _isUserLoggedIn = value;
            OnStateChanged();
        }
    }

    public void SaveLoginedUser(string userId)
    {
        CurrentUserId = userId;
        IsUserLoggedIn = true;
    }

    public void DeleteLoginedUser()
    {
        IsUserLoggedIn = false;
        CurrentUserId = string.Empty;
    }

    public string CurrentUserId { get; private set; }

    public event EventHandler StateChanged;

    protected virtual void OnStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}
