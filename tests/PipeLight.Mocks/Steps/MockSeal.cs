using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class MockSeal : IPipelineSeal<MockPayloadInt>
{
    public Task Execute(MockPayloadInt payload)
    {
        return Task.CompletedTask;
    }
}

public class MockWithResultSeal : IPipelineSeal<MockPipelineResult>
{
    public Task Execute(MockPipelineResult payload)
    {
        return Task.CompletedTask;
    }
}