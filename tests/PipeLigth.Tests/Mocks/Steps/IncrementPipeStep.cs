using PipeLight.Nodes.Steps.Interfaces;

namespace Benchmarks
{
    internal class IncrementPipeStep : IPipeStep<int>
    {
        public async Task<int> ExecuteStepAsync(int payload)
        {
            return await Task.FromResult(payload + 1);
        }
    }
}