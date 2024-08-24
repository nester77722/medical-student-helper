using MedicalStudentHelper.API.Models.CreateModels;
using MedicalStudentHelper.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalStudentHelper.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTestsAsync()
    {
        var result = await _testService.GetAllTestsAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestAsync(string id)
    {
        var result = await _testService.GetTestByIdAsync(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTestAsync(CreateTestModel createTestModel)
    {
        var result = await _testService.AddTestAsync(createTestModel);

        return Ok(result);
    }
}
