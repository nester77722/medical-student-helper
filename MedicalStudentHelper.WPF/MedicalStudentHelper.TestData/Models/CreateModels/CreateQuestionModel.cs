namespace MedicalStudentHelper.TestData.Models.CreateModels;
public class CreateQuestionModel
{
    public string Text { get; set; }
    public List<string> Variants { get; set; }
    public string CorrectAnswer { get; set; }
}
