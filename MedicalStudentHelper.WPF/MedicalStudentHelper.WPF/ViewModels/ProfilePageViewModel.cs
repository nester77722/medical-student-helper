using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalStudentHelper.LocalData.Entities;
using MedicalStudentHelper.TestData.Services.Interfaces;
using MedicalStudentHelper.UserData.Models.CreateModels;
using MedicalStudentHelper.UserData.Models.GetModels;
using MedicalStudentHelper.UserData.Services.Interfaces;
using MedicalStudentHelper.WPF.Models;
using MedicalStudentHelper.WPF.Services.Authentication;
using MedicalStudentHelper.WPF.Services.Interfaces;
using System.Windows;

namespace MedicalStudentHelper.WPF.ViewModels;
public partial class ProfilePageViewModel : ObservableObject
{
    private readonly IAppStateService _appStateService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly GoogleAuthenticator _googleAuthenticator;
    private readonly ITestService _testService;

    public ProfilePageViewModel(GoogleAuthenticator googleAuthenticator, IUserService userService, IAppStateService appStateService, ITestService testService)
    {
        _googleAuthenticator = googleAuthenticator;
        _userService = userService;
        _appStateService = appStateService;

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<GetUserModel, UserModel>()
                    .ForMember(u => u.PictureUrl, opt => opt.MapFrom(x => x.Picture));
        });

        _mapper = mapperConfig.CreateMapper();

        _appStateService.StateChanged += AppStateServiceStateChanged;

        _appStateService.StartCheckingUserLoginState();
        _testService = testService;
    }

    private void AppStateServiceStateChanged(object? sender, EventArgs e)
    {
        if (_appStateService.IsUserLoggedIn)
        {
            AuthorizeMessageVisibility = Visibility.Hidden;
            SignInButtonVisibility = Visibility.Hidden;
            SignOutButtonVisibility = Visibility.Visible;
            UserInfoVisibility = Visibility.Visible;

            _userService.GetUserByIdAsync(_appStateService.CurrentUserId).ContinueWith(result =>
            {
                User = _mapper.Map<UserModel>(result.Result);
            });
        }
        else
        {
            User = null;
            AuthorizeMessageVisibility = Visibility.Visible;
            SignInButtonVisibility = Visibility.Visible;
            UserInfoVisibility = Visibility.Hidden;
            SignOutButtonVisibility = Visibility.Hidden;
        }
    }

    [ObservableProperty]
    private UserModel _user;

    [ObservableProperty]
    private Visibility _signInButtonVisibility;

    [ObservableProperty]
    private Visibility _signOutButtonVisibility;

    [ObservableProperty]
    private Visibility _userInfoVisibility;

    [ObservableProperty]
    private Visibility _authorizeMessageVisibility;

    [RelayCommand]
    private void SignOut()
    {
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

        await _appStateService.SaveLoginedUserAsync(localUser);
    }
}
