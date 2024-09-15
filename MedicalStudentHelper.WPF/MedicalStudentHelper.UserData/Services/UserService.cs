using AutoMapper;
using MedicalStudentHelper.UserData.Entities;
using MedicalStudentHelper.UserData.Models.CreateModels;
using MedicalStudentHelper.UserData.Models.GetModels;
using MedicalStudentHelper.UserData.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MedicalStudentHelper.UserData.Services;
public class UserService : IUserService
{
    private readonly UserContext.UserContext _userContext;
    private readonly IMapper _mapper;

    public UserService(UserContext.UserContext userContext)
    {
        _userContext = userContext;

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateUserFromGoogleAccountModel, User>();
            cfg.CreateMap<User, GetUserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        });

        _mapper = config.CreateMapper();
    }

    public async Task<GetUserModel> CreateUserFromGoogleAccountAsync(CreateUserFromGoogleAccountModel createModel)
    {
        var newUser = _mapper.Map<User>(createModel);

        newUser.Roles = new List<string>
        {
            "User"
        };

        await _userContext.Users.InsertOneAsync(newUser);

        var result = _mapper.Map<GetUserModel>(newUser);

        return result;
    }

    public async Task<GetUserModel> GetUserByGoogleIdAsync(string googleId)
    {
        var filter = Builders<User>.Filter.Eq(u => u.GoogleId, googleId);

        var user = await _userContext.Users.Find(filter).FirstOrDefaultAsync();

        var result = _mapper.Map<GetUserModel>(user);

        return result;
    }

    public async Task<GetUserModel> GetUserByIdAsync(string id)
    {
        ObjectId objectId = ObjectId.Parse(id);

        var filter = Builders<User>.Filter.Eq(u => u.Id, objectId);

        var user = await _userContext.Users.Find(filter).FirstOrDefaultAsync();

        var result = _mapper.Map<GetUserModel>(user);

        return result;
    }
}
