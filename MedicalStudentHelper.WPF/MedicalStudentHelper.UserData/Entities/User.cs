using MongoDB.Bson;

namespace MedicalStudentHelper.UserData.Entities;
public class User
{
    public ObjectId Id { get; set; }
    public string GoogleId { get; set; }
    public string Name { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string Picture { get; set; }
}
