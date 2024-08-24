using MedicalStudentHelper.WPF.ViewModels;
using System.Windows;

namespace MedicalStudentHelper.WPF.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        DataContext = _viewModel;


        Loaded += MainWindow_Loaded;
        //_appStateService.StateChanged += OnStateChanged;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
    }
}