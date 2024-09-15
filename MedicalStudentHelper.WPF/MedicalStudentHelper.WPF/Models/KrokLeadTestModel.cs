namespace MedicalStudentHelper.WPF.Models;
public class KrokLeadTestModel
{
    public string Id { get; set; }
    public string Question { get; set; }
    public List<string> Variants { get; set; }
    public string Answer { get; set; }
    public bool EnAvailable { get; set; }
    public bool Done { get; set; }
    public bool Saved { get; set; }
    public int Comments { get; set; }
}

