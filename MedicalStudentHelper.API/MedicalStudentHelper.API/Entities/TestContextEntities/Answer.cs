using MongoDB.Bson;

namespace MedicalStudentHelper.API.Entities.TestContextEntities;

public class Answer
{
    public ObjectId Id { get; set; }
    public string Text { get; set; }
}
