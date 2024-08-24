using MedicalStudentHelper.WPF.Services.Interfaces;

namespace MedicalStudentHelper.WPF.Services;
public class TestService : ITestService
{
    public string GetData()
    {
        return "Data from Test Service";
    }
}
