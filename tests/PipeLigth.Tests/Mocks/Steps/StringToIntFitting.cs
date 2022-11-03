using PipeLight.Nodes.Steps.Interfaces;

namespace Benchmarks
{
    internal class StringToIntTransform : IPipeTransform<string, int>
    {
        public async Task<int> TransformAsync(string payload)
        {
            return await Task.FromResult(int.Parse(payload));
        }
    }
}