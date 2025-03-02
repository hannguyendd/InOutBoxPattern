using InboxService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace InboxService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResponse>), 200)]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await userService.GetAllAsync());
    }
    
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(UserResponse), 200)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid userId)
    {
        return Ok(await userService.GetAsync(userId));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(UserResponse), 201)]
    public async Task<IActionResult> AddAsync([FromBody] SaveUserRequest request)
    {
        return Created(string.Empty, await userService.AddAsync(request));
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(typeof(UserResponse), 200)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid userId, [FromBody] SaveUserRequest request)
    {
        return Ok(await userService.UpdateAsync(userId, request));
    }
}