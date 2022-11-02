using PipeLight.Pipes.Interfaces;

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