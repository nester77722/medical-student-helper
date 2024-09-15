using MedicalStudentHelper.WPF.ViewModels.Admin;
using System.Windows.Controls;

namespace MedicalStudentHelper.WPF.Views.Admin;
/// <summary>
/// Interaction logic for TestCategoryDetailsUserControl.xaml
/// </summary>
public partial class TestCategoryDetailsUserControl : UserControl
{
    public TestCategoryDetailsUserControl(TestCategoryDetailsViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}
