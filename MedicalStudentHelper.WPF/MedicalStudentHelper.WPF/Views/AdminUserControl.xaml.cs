using MedicalStudentHelper.WPF.ViewModels;
using System.Windows.Controls;

namespace MedicalStudentHelper.WPF.Views
{
    /// <summary>
    /// Interaction logic for AdminUserControl.xaml
    /// </summary>
    public partial class AdminUserControl : UserControl
    {
        public AdminUserControl(AdminViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
