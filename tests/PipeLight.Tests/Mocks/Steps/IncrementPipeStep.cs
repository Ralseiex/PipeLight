using PipeLight.Abstractions.Steps;

namespace PipeLight.Tests.Mocks.Steps;

internal class IncrementPipeStep : IPipelineStep<int>
{
    public async Task<int> Execute(int payload)
    {
        return await Task.FromResult(payload + 1);
    }
}