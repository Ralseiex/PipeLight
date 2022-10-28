using PipeLight.Base;
using PipeLight.Interfaces.Steps;

namespace PipeLight;

public class SealedPipeline<TIn> : PipelineBase<TIn>
{
    public SealedPipeline(IPipelineStepEnter<TIn> firstStep)
    {
        FirstStep = firstStep;
    }
}