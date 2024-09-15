namespace MedicalStudentHelper.WPF.Models;
public class TestCategoryModel : ICloneable
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public string ShortDescription => $"{Name} - {Year}";

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public override bool Equals(object obj)
    {
        if (obj is TestCategoryModel category)
        {
            return Id == category.Id &&
                   Name == category.Name &&
                   Description == category.Description &&
                   Year == category.Year;
        }
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(Id, Name, Description, Year);
}
