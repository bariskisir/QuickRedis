using Bogus;
using Microsoft.AspNetCore.Mvc;
using QuickRedis.Sample.WebApi.Models;
using QuickRedis.Services.Definitions;
namespace QuickRedis.Sample.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IQuickRedisService _quickRedisService;
    public UserController(IQuickRedisService quickRedisService)
    {
        _quickRedisService = quickRedisService;
    }
    [Route("GetUsers")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var userList = this.GetUserList(500);
        return Ok(userList);
    }
    [Route("GetUsersCached")]
    [HttpGet]
    public async Task<IActionResult> GetUsersCached()
    {
        var userList = await _quickRedisService.AddOrGetFuncAsync<List<User>>("users_unique_key", () => this.GetUserList(500), 10);
        return Ok(userList);
    }
    private List<User> GetUserList(int count)
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Name, f => f.Name.FirstName())
            .RuleFor(u => u.Surname, f => f.Name.LastName());
        var userList = userFaker.Generate(count);
        return userList;
    }
}
