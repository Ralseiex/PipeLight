using PipeLight.Base;
using PipeLight.Interfaces;
using PipeLight.Middlewares;

namespace PipeLight;

public class Pipeline<TIn> : PipelineBase<TIn, TIn>
{
    public Pipeline()
    {
        var defaultMiddleware = new DefaultMiddleware<TIn>();
        var firstStep = new PipelineStep<TIn, TIn>(defaultMiddleware);
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