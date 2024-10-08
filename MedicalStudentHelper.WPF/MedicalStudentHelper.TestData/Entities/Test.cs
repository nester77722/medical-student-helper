﻿using MongoDB.Bson;

namespace MedicalStudentHelper.TestData.Entities;

public class Test
{
    public ObjectId Id { get; set; }
    public ObjectId CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Question> Questions { get; set; }
}
