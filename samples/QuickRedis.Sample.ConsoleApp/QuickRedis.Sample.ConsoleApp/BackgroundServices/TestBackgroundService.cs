using Bogus;
using Microsoft.Extensions.Hosting;
using QuickRedis.Sample.ConsoleApp.Models;
using QuickRedis.Services.Definitions;
namespace QuickRedis.Sample.ConsoleApp.BackgroundServices;
public class TestBackgroundService : BackgroundService
{
    private readonly IQuickRedisService _quickRedisService;
    public TestBackgroundService(IQuickRedisService quickRedisService)
    {
        _quickRedisService = quickRedisService;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Generating 1000 users...");
        var before = DateTime.UtcNow;
        var userList = _quickRedisService.AddOrGetFuncAsync<List<User>>("users_unique_key", () => this.GetUserList(1000), 60).Result;
        var after = DateTime.UtcNow;
        Console.WriteLine($"Done in {(after - before).TotalSeconds} seconds");
        while (true)
        {
            Console.WriteLine("Press enter key to get from cache");
            Console.ReadKey();
            var beforeCached = DateTime.UtcNow;
            var userListCached = _quickRedisService.AddOrGetFuncAsync<List<User>>("users_unique_key", () => this.GetUserList(1000), 60).Result;
            var afterCached = DateTime.UtcNow;
            Console.WriteLine($"Done in {(afterCached - beforeCached).TotalSeconds} seconds");
        }
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
