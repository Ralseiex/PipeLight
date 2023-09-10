using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class ActionWithException<TException> : IPipelineAction<MockPayloadInt> where TException : Exception, new()
{
    public Task<MockPayloadInt> Execute(MockPayloadInt payload) => throw new TException();
}