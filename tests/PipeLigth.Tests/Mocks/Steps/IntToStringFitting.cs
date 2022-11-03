using PipeLight.Nodes.Steps.Interfaces;

namespace Benchmarks
{
    internal class IntToStringTransform : IPipeTransform<int, string>
    {
        public async Task<string> TransformAsync(int payload)
        {
            return await Task.FromResult(payload.ToString());
        }
    }
}