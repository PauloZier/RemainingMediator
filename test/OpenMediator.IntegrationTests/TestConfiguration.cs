using Microsoft.Extensions.DependencyInjection;

namespace OpenMediator.IntegrationTests;

public class TestConfiguration
{
    protected readonly IServiceProvider _serviceProvider;

    public TestConfiguration()
    {
        _serviceProvider = new ServiceCollection()
            .AddOpenMediator(assemblies: AppDomain.CurrentDomain.GetAssemblies())
            .BuildServiceProvider();
    }    
}
