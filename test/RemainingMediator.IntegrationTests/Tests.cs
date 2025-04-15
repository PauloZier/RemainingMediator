using Microsoft.Extensions.DependencyInjection;

using RemainingMediator.IntegrationTests.Commands;

namespace RemainingMediator.IntegrationTests;

public class Tests : TestConfiguration
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("Hello", "2025-04-12")]
    [TestCase("Message", "2025-04-01")]
    public async Task TestCommandHandler(string description, DateTime dateTime)
    {
        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var result = await mediator.SendAsync(new TestCommand(description, dateTime));

        Assert.Pass();
    }
}