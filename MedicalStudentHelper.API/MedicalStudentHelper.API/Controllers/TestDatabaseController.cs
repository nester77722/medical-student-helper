using MedicalStudentHelper.API.Entities.Contexts;
using MedicalStudentHelper.API.Entities.Contexts.UserContex;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MedicalStudentHelper.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestDatabaseController : ControllerBase
{
    private readonly UserContext _userContext;

    public TestDatabaseController(UserContext userContext)
    {
        _userContext = userContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetValues()
    {
        var result = await _userContext.Users.Find(new BsonDocument()).ToListAsync();

        return Ok(result);
    }
}
