using MedicalStudentHelper.WPF.Services.Interfaces;
using System.Windows.Controls;

namespace MedicalStudentHelper.WPF.Services;
public class NavigationService : INavigationService
{
    private readonly Stack<UserControl> _navigationStack = new Stack<UserControl>();
    public event Action<UserControl> CurrentContentChanged; // Событие для уведомления о смене содержимого

    public UserControl CurrentContent { get => _navigationStack.FirstOrDefault(); }
    public bool CanGoBack { get => _navigationStack.Count > 1; }

    public void NavigateTo(UserControl newContent)
    {
        _navigationStack.Push(newContent); // Сохраняем текущее состояние перед навигацией

        CurrentContentChanged?.Invoke(CurrentContent); // Уведомляем об изменении содержимого
    }

    public void GoBack()
    {
        if (CanGoBack)
        {
            _navigationStack.Pop();
            CurrentContentChanged?.Invoke(CurrentContent); // Уведомляем об изменении содержимого
        }
    }
}
