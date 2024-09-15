using MedicalStudentHelper.WPF.Models;
using MedicalStudentHelper.WPF.ViewModels.Admin;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedicalStudentHelper.WPF.Views.Admin;
/// <summary>
/// Interaction logic for TestCategoriesUserControl.xaml
/// </summary>
public partial class TestCategoriesUserControl : UserControl
{
    public TestCategoriesUserControl(TestCategoriesViewModel testCategoriesViewModel)
    {
        InitializeComponent();

        DataContext = testCategoriesViewModel;
    }

    private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var listBoxItem = sender as ListBoxItem;
        if (listBoxItem != null && listBoxItem.DataContext is TestCategoryModel selectedCategory)
        {
            var viewModel = (TestCategoriesViewModel)DataContext;
            viewModel.RedirectToCategoryCommand.Execute(selectedCategory);
        }
    }
}
