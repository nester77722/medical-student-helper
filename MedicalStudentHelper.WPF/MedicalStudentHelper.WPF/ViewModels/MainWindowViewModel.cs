using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using MedicalStudentHelper.LocalData.Services;
using MedicalStudentHelper.UserData.Models.GetModels;
using MedicalStudentHelper.UserData.Services.Interfaces;
using MedicalStudentHelper.WPF.Models;
using MedicalStudentHelper.WPF.Services;
using MedicalStudentHelper.WPF.Services.Authentication;
using MedicalStudentHelper.WPF.Services.Interfaces;
using System.Windows.Controls;

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

        CurrentPage = _viewsFactory.GetProfilePage();

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<GetUserModel, UserModel>()
                    .ForMember(u => u.PictureUrl, opt => opt.MapFrom(x => x.Picture));
        });

        _mapper = mapperConfig.CreateMapper();
        //CurrentUser = new UserModel();

        //_appStateService.StateChanged += AppStateChanged;

        //CheckUserLoginState().Wait();
    }

    [ObservableProperty]
    private Page _currentPage;
}
