using MedicalStudentHelper.WPF.ViewModels;
using System.Windows.Controls;

namespace MedicalStudentHelper.WPF.Views;
/// <summary>
/// Interaction logic for ProfilePage.xaml
/// </summary>
public partial class ProfileUserControl : UserControl
{
    public ProfileUserControl(ProfilePageViewModel profilePageViewModel)
    {
        InitializeComponent();

        DataContext = profilePageViewModel;
    }
}
