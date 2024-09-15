namespace MedicalStudentHelper.UserData.Models.GetModels;
public class GetUserModel
{
    public string Id { get; set; }
    public string GoogleId { get; set; }
    public string Name { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string Picture { get; set; }
    public List<string> Roles { get; set; }
}
