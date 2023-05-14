using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class IntToStringTransform : IPipelineTransform<int, string>
{
    public Task<string> Transform(int payload)
    {
        return Task.FromResult(payload.ToString());
    }
}