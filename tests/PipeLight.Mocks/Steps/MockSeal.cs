using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class MockSeal : IPipelineSealedStep<MockPayloadInt>
{
    public Task Execute(MockPayloadInt payload)
    {
        return Task.CompletedTask;
    }
}

public class MockWithResultSeal : IPipelineSealedStep<MockPipelineResult>
{
    public Task Execute(MockPipelineResult payload)
    {
        return Task.CompletedTask;
    }
}
