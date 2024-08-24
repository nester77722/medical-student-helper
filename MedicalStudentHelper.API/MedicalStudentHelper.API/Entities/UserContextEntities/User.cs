using MongoDB.Bson;

namespace MedicalStudentHelper.API.Entities.UserContextEntities;

public class User
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
}
