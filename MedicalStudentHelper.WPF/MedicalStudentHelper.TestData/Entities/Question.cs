﻿using MongoDB.Bson;

namespace MedicalStudentHelper.TestData.Entities;

public class Question
{
    public ObjectId Id { get; set; }
    public string Text { get; set; }
    public List<string> Variants { get; set; }
    public string CorrectAnswer { get; set; }
}
