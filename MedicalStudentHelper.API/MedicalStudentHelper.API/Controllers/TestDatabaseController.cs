using MedicalStudentHelper.API.Entities.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalStudentHelper.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestDatabaseController : ControllerBase
{
    private readonly TestContext testContext;

    public TestDatabaseController(TestContext testContext)
    {
        this.testContext = testContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetValues()
    {
        var result = await testContext.Users.ToListAsync();

        return Ok(result);
    }
}
