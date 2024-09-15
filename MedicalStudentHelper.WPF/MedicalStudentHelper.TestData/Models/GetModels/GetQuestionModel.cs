namespace MedicalStudentHelper.TestData.Models.GetModels;
public class GetQuestionModel
{
    public string Id { get; set; }
    public string TestId { get; set; }
    public string Text { get; set; }
    public string CorrectAnswer { get; set; }
    public List<string> Variants { get; set; }
}
