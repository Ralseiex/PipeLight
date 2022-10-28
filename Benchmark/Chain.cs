using PipelineNet.ChainsOfResponsibility;
using PipelineNet.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    internal class Chain : IMiddleware<int, int>
    {
        public int Run(int parameter, Func<int, int> next)
        {
            return next(parameter + 1);
        }
    }
    internal class AsyncChain : IAsyncMiddleware<int, int>
    {
        public async Task<int> Run(int parameter, Func<int, Task<int>> next)
        {
            return await next(parameter + 1);
        }
    }
}