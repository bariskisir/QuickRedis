![QuickRedis](https://raw.githubusercontent.com/bariskisir/QuickRedis/master/assets/logo.png)

# QuickRedis

QuickRedis is a library that help you implement Redis quickly with useful methods.

![Nuget](https://img.shields.io/nuget/v/QuickRedis?label=QuickRedis)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## How to use
Install `QuickRedis` from [Nuget](https://www.nuget.org/packages/QuickRedis)

```bash
dotnet add package QuickRedis
nuget install QuickRedis
```

```csharp
builder.Services.AddQuickRedis(options => {
	options.Configuration = "localhost";
	options.InstanceName = "main_";
});
```

```csharp
private readonly IQuickRedisService _quickRedisService;

public TestController(IQuickRedisService quickRedisService)
{
    _quickRedisService = quickRedisService;
}
```

### Run redis from docker
```bash
# w/ gui
docker run -d --name redis_stack --restart unless-stopped -p 6379:6379 -p 8001:8001 -v redis_data:/data redis/redis-stack:latest

# w/o gui
docker run -d --name redis_stack_server --restart unless-stopped -p 6379:6379 -v redis_data:/data redis/redis-stack-server:latest
```

### Methods
```csharp
// GetAsync
var product = await _quickRedisService.GetAsync<Product>("unique_product_id");
var product = _quickRedisService.GetAsync<Product>("unique_product_id").Result;

// SetAsync
await _quickRedisService.SetAsync("unique_product_id", product);
_quickRedisService.SetAsync("unique_product_id", product).Wait();

// RemoveAsync
await _quickRedisService.RemoveAsync("unique_product_id");
_quickRedisService.RemoveAsync("unique_product_id").Wait();

// AddOrGetFuncAsync
List<string> TestMethod();
var methodResult = await _quickRedisService.AddOrGetFuncAsync<List<string>>("result_unique_key", () => TestMethod());
var methodResult = _quickRedisService.AddOrGetFuncAsync<List<string>>("result_unique_key", () => TestMethod()).Result;
```

### Samples

[Web Api](https://github.com/bariskisir/QuickRedis/tree/master/samples/QuickRedis.Sample.WebApi)

[Console App](https://github.com/bariskisir/QuickRedis/tree/master/samples/QuickRedis.Sample.ConsoleApp)


