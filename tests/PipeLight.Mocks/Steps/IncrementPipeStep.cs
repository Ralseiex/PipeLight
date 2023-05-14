using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class IncrementPipeStep : IPipelineStep<int>
{
    public async Task<int> Execute(int payload)
    {
        return await Task.FromResult(payload + 1);
    }
}