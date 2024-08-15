using MongoDB.Bson;

namespace MedicalStudentHelper.API.Entities.Contexts.UserContex;

public class User
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
}
