namespace MedicalStudentHelper.TestData.Models.GetModels;

public class GetTestWithQuestionsModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<GetQuestionModel> Questions { get; set; }
}
