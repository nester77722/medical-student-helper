﻿namespace MedicalStudentHelper.TestData.Models.CreateModels;

public class CreateTestModel
{
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<CreateQuestionModel> Questions { get; set; }
}
