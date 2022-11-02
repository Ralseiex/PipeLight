using PipeLight.Pipes.Interfaces;

namespace Benchmarks
{
    internal class StringToIntFitting : IPipeFitting<string, int>
    {
        public async Task<int> FitAsync(string payload)
        {
            return await Task.FromResult(int.Parse(payload));
        }
    }
}