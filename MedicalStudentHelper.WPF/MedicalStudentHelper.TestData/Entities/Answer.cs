using MongoDB.Bson;

namespace MedicalStudentHelper.TestData.Entities;

public class Answer
{
    public ObjectId Id { get; set; }
    public string Text { get; set; }
}
