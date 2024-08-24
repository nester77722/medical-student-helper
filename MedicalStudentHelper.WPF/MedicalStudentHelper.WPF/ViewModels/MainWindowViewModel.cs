using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalStudentHelper.LocalData.Entities;
using MedicalStudentHelper.LocalData.Services;
using MedicalStudentHelper.UserData.Entities;
using MedicalStudentHelper.UserData.Models.CreateModels;
using MedicalStudentHelper.UserData.Models.GetModels;
using MedicalStudentHelper.UserData.Services.Interfaces;
using MedicalStudentHelper.WPF.Models;
using MedicalStudentHelper.WPF.Services;
using MedicalStudentHelper.WPF.Services.Authentication;
using MedicalStudentHelper.WPF.Services.Interfaces;
using System.Windows;

namespace MedicalStudentHelper.WPF.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IAppStateService _appStateService;
    private readonly IUserService _userService;
    private readonly IUserLocalService _userLocalService;
    private readonly IMapper _mapper;
    private readonly GoogleAuthenticator _googleAuthenticator;
    private readonly ViewsFactory _viewsFactory;

    public MainWindowViewModel(IAppStateService appStateService, IUserService userService, IUserLocalService userLocalService, ViewsFactory viewsFactory, GoogleAuthenticator googleAuthenticator)
    {
        _appStateService = appStateService;
        _userService = userService;
        _userLocalService = userLocalService;
        _viewsFactory = viewsFactory;
        _googleAuthenticator = googleAuthenticator;

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<GetUserModel, UserModel>()
                    .ForMember(u => u.PictureUrl, opt => opt.MapFrom(x => x.Picture));
        });

        _mapper = mapperConfig.CreateMapper();
        CurrentUser = new UserModel();

        _appStateService.StateChanged += AppStateChanged;

        CheckUserLoginState().Wait();
    }

    [ObservableProperty]
    private UserModel _currentUser;

    [ObservableProperty]
    private Visibility _signInButtonVisibility;

    [ObservableProperty]
    private Visibility _signOutButtonVisibility;

    [ObservableProperty]
    private Visibility _profileButtonVisibility;

    private async void AppStateChanged(object? sender, EventArgs e)
    {
        if (_appStateService.IsUserLoggedIn)
        {
            var user = await _userService.GetUserByIdAsync(_appStateService.CurrentUserId);

            CurrentUser = _mapper.Map<UserModel>(user);

            SignInButtonVisibility = Visibility.Collapsed;
            SignOutButtonVisibility = Visibility.Visible;
            ProfileButtonVisibility = Visibility.Visible;
        }
        else
        {
            CurrentUser = null;
            
            SignInButtonVisibility = Visibility.Visible;
            SignOutButtonVisibility = Visibility.Collapsed;
            ProfileButtonVisibility = Visibility.Collapsed;
        }
    }

    [RelayCommand]
    private void SignOut()
    {
        _userLocalService.DeleteUser();
        _appStateService.DeleteLoginedUser();
    }

    [RelayCommand]
    private async Task SignIn(CancellationToken cancellationToken)
    {
        var authResult = await _googleAuthenticator.StartAuthentication(cancellationToken);

        var user = await _userService.GetUserByGoogleIdAsync(authResult.Sub);

        if (user == null)
        {
            var newUser = new CreateUserFromGoogleAccountModel()
            {
                GoogleId = authResult.Sub,
                FamilyName = authResult.FamilyName,
                GivenName = authResult.GivenName,
                Name = authResult.Name,
                Picture = authResult.Picture,
            };

            user = await _userService.CreateUserFromGoogleAccountAsync(newUser);
        }

        var localUser = new LocalUser
        {
            Id = user.Id,
            GoogleId = user.GoogleId,
            LastLoginTime = DateTime.UtcNow
        };

        await _userLocalService.SaveUserAsync(localUser);

        _appStateService.SaveLoginedUser(user.Id);
    }

    private async Task CheckUserLoginState()
    {
        if (await _userLocalService.IsUserLoggedInAsync())
        {
            var user = await _userLocalService.GetUserAsync();

            _appStateService.SaveLoginedUser(user.Id);
        }
        else
        {
            _appStateService.DeleteLoginedUser();
        }
    }
}
