using MedicalStudentHelper.LocalData.Entities;
using MedicalStudentHelper.LocalData.Services;
using MedicalStudentHelper.TestData.Entities;
using MedicalStudentHelper.TestData.TestContext;
using MedicalStudentHelper.UserData.Models.CreateModels;
using MedicalStudentHelper.UserData.Services.Interfaces;
using MedicalStudentHelper.WPF.Helpers;
using MedicalStudentHelper.WPF.Services.Authentication;
using MedicalStudentHelper.WPF.Services.Interfaces;
using MedicalStudentHelper.WPF.ViewModels;
using MongoDB.Driver;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Media.Imaging;

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