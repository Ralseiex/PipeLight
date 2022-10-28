using PipeLight.Base;
using PipeLight.Interfaces;
using PipeLight.Interfaces.Steps;
using PipeLight.Steps.DefaultSteps;
using PipeLight.Steps.Delegates;
using PipeLight.Steps.Interfaces;
using System.Threading;

namespace PipeLight;

public class MonoTypePipeline<T> : IPipeline<T>
{
    private readonly List<IPipelineStepAsyncHandler<T, T>> _steps = new();

    public IPipeline<T> AddStep(IPipelineStepAsyncHandler<T, T> stepHandler)
    {
        _steps.Add(stepHandler);
        return this;
    }

    public IPipeline<T, TNewOut> AddStep<TNewOut>(IPipelineStepAsyncHandler<T, TNewOut> stepHandler)
    {
        throw new NotImplementedException();
    }

    public ISealedPipeline<T> AddStep(IPipelineStepAsyncHandler<T> lastStepHandler)
    {
        throw new NotImplementedException();
    }

    public async Task<T> PushAsync(T payload)
    {
        var result = payload;
        foreach (var step in _steps)
        {
            result = await step.InvokeAsync(result);
        }
        return result;
    }
}
public class Pipeline
{
    public IPipeline<TIn, TNewOut> AddStep<TIn, TNewOut>(IPipelineStepAsyncHandler<TIn, TNewOut> stepHandler)
    {
        var firstStep = new PipelineStep<TIn, TNewOut>(stepHandler);
        return new Pipeline<TIn, TNewOut>(firstStep, firstStep);
    }
    public ISealedPipeline<TIn> AddStep<TIn>(IPipelineStepAsyncHandler<TIn> lastStepHandler)
    {
        var singleStep = new PipelineStep<TIn>(lastStepHandler);
        return new SealedPipeline<TIn>(singleStep);
    }
}
public class Pipeline<TIn> : PipelineBase<TIn, TIn>
{
    public Pipeline()
    {
        var defaultStep = new DefaultStepAsync<TIn>();
        var firstStep = new PipelineStep<TIn, TIn>(defaultStep);
        FirstStep = firstStep;
        LastStep = firstStep;
    }
}
public class Pipeline<TIn, TOut> : PipelineBase<TIn, TOut>
{
    public Pipeline(IPipelineStepEnter<TIn> firstStep, IPipelineStepExit<TOut> lastStep)
        : base(firstStep, lastStep)
    {
    }
}