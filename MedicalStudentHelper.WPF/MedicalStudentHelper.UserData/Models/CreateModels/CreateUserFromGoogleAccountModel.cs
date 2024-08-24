namespace MedicalStudentHelper.UserData.Models.CreateModels;
public class CreateUserFromGoogleAccountModel
{
    public string GoogleId { get; set; }
    public string Name { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string Picture { get; set; }
}
