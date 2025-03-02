using Microsoft.AspNetCore.Mvc;
using OutboxService.Services;
using Shared.Contracts;

namespace OutboxService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController(PatientService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatientResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        return Ok(await service.GetAllAsync());
    }
}