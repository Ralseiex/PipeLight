using PipeLight.Steps.Interfaces;

namespace Benchmarks
{
    internal class MockStepAsync : IPipelineStepAsyncHandler<int, int>
    {
        public async Task<int> InvokeAsync(int payload)
        {
            return await Task.FromResult(payload + 1);
        }
    }
}