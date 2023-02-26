using PipeLight.Abstractions.Steps;

namespace PipeLight.Tests.Mocks.Steps;

internal class StringToIntTransform : IPipelineTransformStep<string, int>
{
    public async Task<int> Transform(string payload)
    {
        return await Task.FromResult(int.Parse(payload));
    }
}