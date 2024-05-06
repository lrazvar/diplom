using DataBaseService.Db.Entity;
using DataBaseService.Db.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DataBaseService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserRepository userRepository) : ControllerBase
{
    [HttpGet("getLastTenData")]
    [SwaggerOperation(
        Summary = "Get last ten data from the database",
        Description = "Returns a list of the last ten records from the database."
    )]
    [SwaggerResponse(200, "OK", typeof(List<UserData>))]
    public async Task<IActionResult> GetLastTenData()
    {
        var lastTenRecords = await userRepository.GetLastTenRecordsAsync();
        return Ok(lastTenRecords);
    }
}