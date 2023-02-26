using PipeLight.Abstractions.Steps;

namespace PipeLight.Tests.Mocks.Steps;

internal class IntToStringTransform : IPipelineTransformStep<int, string>
{
    public Task<string> Transform(int payload)
    {
        return Task.FromResult(payload.ToString());
    }
}