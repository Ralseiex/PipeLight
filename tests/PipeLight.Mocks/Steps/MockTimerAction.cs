using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class MockTimerAction : IPipelineAction<MockPayloadInt>
{
    private readonly TimeSpan _delay;

    public MockTimerAction(TimeSpan delay)
    {
        _delay = delay;
    }
    public async Task<MockPayloadInt> Execute(MockPayloadInt payload)
    {
        await Task.Delay(_delay);
        return payload;
    }
}