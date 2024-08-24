using MongoDB.Bson;

namespace MedicalStudentHelper.API.Entities.TestContextEntities;

public class Test
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Question> Questions { get; set; }
    public int Year {  get; set; }
}
