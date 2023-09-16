using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickRedis.Extensions;
using QuickRedis.Sample.ConsoleApp.BackgroundServices;
namespace QuickRedis.Sample.ConsoleApp;
public class Program
{
    static void Main(string[] args)
    {
        Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddQuickRedis(options =>
                    {
                        options.Configuration = "localhost";
                        options.InstanceName = "main_";
                    });
                    services.AddHostedService<TestBackgroundService>();
                })
                .Build()
                .Run();
    }
}