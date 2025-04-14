namespace OpenMediator.IntegrationTests.Commands;

public class TestCommandHandler : IRequestHandler<TestCommand, TestCommandResult>
{
    public Task<TestCommandResult> HandleAsync(TestCommand request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new TestCommandResult());
    }
}
