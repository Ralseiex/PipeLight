using Benchmark;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PipeLight;
using PipeLight.Extensions;
using PipeLight.Interfaces;
using PipelineNet.ChainsOfResponsibility;
using PipelineNet.MiddlewareResolver;
using System.Security.Cryptography;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class PipelineBench
    {
        private readonly IResponsibilityChain<int, int> chain = new ResponsibilityChain<int, int>(new ActivatorMiddlewareResolver())
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
            ;
        private readonly IAsyncResponsibilityChain<int, int> asyncChain = new AsyncResponsibilityChain<int, int>(new ActivatorMiddlewareResolver())
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
            ;

        [Benchmark]
        public void RespChainCreation()
        {
            for (int i = 0; i < 100; i++)
            {
                IResponsibilityChain<int, int> chain = new ResponsibilityChain<int, int>(new ActivatorMiddlewareResolver())
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                .Chain<Chain>()
                ;
            }
        }
        [Benchmark]
        public void AsyncRespChainCreation()
        {
            for (int i = 0; i < 100; i++)
            {
                IAsyncResponsibilityChain<int, int> asyncChain = new AsyncResponsibilityChain<int, int>(new ActivatorMiddlewareResolver())
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                .Chain<AsyncChain>()
                ;
            }
        }
        [Benchmark]
        public void RespChainExecution()
        {
            for (int i = 0; i < 100; i++)
            {
                chain.Execute(0);
            }
        }
        [Benchmark]
        public async Task AsyncRespChainExecution()
        {
            for (int i = 0; i < 100; i++)
            {
                await asyncChain.Execute(0);
            }
        }
        [Benchmark]
        public void RespChainParralelExecution()
        {
            Parallel.For(0, 100, (i) =>
            {
                chain.Execute(0);
            });
        }
        [Benchmark]
        public void AsyncRespChainParralelExecution()
        {
            Parallel.For(0, 100, async (i) =>
            {
                await asyncChain.Execute(0);
            });
        }


        private readonly IPipeline<int, int> intLambdaPipeline = new Pipeline()
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
            ;
        private readonly IPipeline<int, int> intPipelineAsync = new Pipeline()
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
            ;

        [Benchmark]
        public void PipeLightLambdaCreation()
        {
            for (int i = 0; i < 100; i++)
            {
                IPipeline<int, int> intLambdaPipeline = new Pipeline()
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1)
                .AddStep((int x) => x + 1);
                ;
            }
        }
        [Benchmark]
        public void PipeLightCreation()
        {
            for (int i = 0; i < 100; i++)
            {
                IPipeline<int, int> intPipelineAsync = new Pipeline()
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                .AddStep(new MockStepAsync())
                ;
            }
        }
        [Benchmark]
        public async Task PipeLightLambdaExecution()
        {
            for (int i = 0; i < 100; i++)
            {
                await intLambdaPipeline.PushAsync(0);
            }
        }
        [Benchmark]
        public async Task PipeLightExecution()
        {
            for (int i = 0; i < 100; i++)
            {
                await intPipelineAsync.PushAsync(0);
            }
        }
        [Benchmark]
        public void PipeLightLambdaParralelExecution()
        {
            Parallel.For(0, 100, async (i) =>
            {
                await intLambdaPipeline.PushAsync(0);
            });
        }
        [Benchmark]
        public void PipeLightParralelExecution()
        {
            Parallel.For(0, 100, async (i) =>
            {
                await intPipelineAsync.PushAsync(0);
            });
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}