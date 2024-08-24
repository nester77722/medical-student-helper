using MedicalStudentHelper.WPF.ViewModels;
using System.Windows.Controls;

namespace MedicalStudentHelper.WPF.Views;
/// <summary>
/// Interaction logic for ProfilePage.xaml
/// </summary>
public partial class ProfilePage : Page
{
    public ProfilePage(ProfilePageViewModel profilePageViewModel)
    {
        InitializeComponent();

        DataContext = profilePageViewModel;
    }
}
