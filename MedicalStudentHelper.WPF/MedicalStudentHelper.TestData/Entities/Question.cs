using MongoDB.Bson;

namespace MedicalStudentHelper.TestData.Entities;

public class Question
{
    public ObjectId Id { get; set; }
    public ObjectId TestId { get; set; }
    public string Text { get; set; }
    public ObjectId CorrectAnswerId { get; set; }
    public List<Answer> Answers { get; set; }
}
