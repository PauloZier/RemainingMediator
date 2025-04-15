namespace RemainingMediator.IntegrationTests.Commands;

public record TestCommand(string Description, DateTime dateTime) 
    : IRequest<TestCommandResult>;
