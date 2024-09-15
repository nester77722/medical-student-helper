using System.Windows.Controls;

namespace MedicalStudentHelper.WPF.Services.Interfaces;
public interface INavigationService
{
    UserControl CurrentContent { get; }
    bool CanGoBack { get; }

    event Action<UserControl> CurrentContentChanged;

    void GoBack();
    void NavigateTo(UserControl newContent);
}
