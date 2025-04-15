using Microsoft.Extensions.DependencyInjection;

namespace RemainingMediator.IntegrationTests;

public class TestConfiguration
{
    protected readonly IServiceProvider _serviceProvider;

    public TestConfiguration()
    {
        _serviceProvider = new ServiceCollection()
            .AddRemainingMediator(assemblies: AppDomain.CurrentDomain.GetAssemblies())
            .BuildServiceProvider();
    }    
}
