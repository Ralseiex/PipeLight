using PipeLight.Pipes.Interfaces;

namespace Benchmarks
{
    internal class IntToStringFitting : IPipeFitting<int, string>
    {
        public async Task<string> FitAsync(int payload)
        {
            return await Task.FromResult(payload.ToString());
        }
    }
}