using MedicalStudentHelper.WPF.ViewModels.Admin;
using System.Windows.Controls;

namespace MedicalStudentHelper.WPF.Views.Admin;
/// <summary>
/// Interaction logic for InserTestFromKrokLeadView.xaml
/// </summary>
public partial class InsertTestFromKrokLeadUserControl : UserControl
{
    public InsertTestFromKrokLeadUserControl(InsertTestFromKrokLeadViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}
