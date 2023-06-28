using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class StepWithException<TException> : IPipelineStep<MockPayloadInt> where TException : Exception, new()
{
    public Task<MockPayloadInt> Execute(MockPayloadInt payload) => throw new TException();
}
