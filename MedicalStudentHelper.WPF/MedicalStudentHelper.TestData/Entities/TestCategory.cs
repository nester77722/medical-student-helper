using MongoDB.Bson;

namespace MedicalStudentHelper.TestData.Entities;
public class TestCategory
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
}
