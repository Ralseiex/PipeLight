using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class StringToIntTransform : IPipelineTransform<string, int>
{
    public async Task<int> Transform(string payload)
    {
        return await Task.FromResult(int.Parse(payload));
    }
}