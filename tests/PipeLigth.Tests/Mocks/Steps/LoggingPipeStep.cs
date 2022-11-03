using PipeLight.Nodes.Steps.Interfaces;
using System.Diagnostics;

namespace Benchmarks
{
    internal class LoggingPipeStep : IPipeStep<string>
    {
        public async Task<string> ExecuteStepAsync(string payload)
        {
            Console.WriteLine(payload);
            Debug.WriteLine(payload);
            return await Task.FromResult(payload);
        }
    }
}