using PipeLight.Base;
using PipeLight.Nodes.Interfaces;

namespace PipeLight;

public class SealedPipeline<TIn> : PipelineBase<TIn>
{
    public SealedPipeline(IPipelineEnter<TIn> firstStep)
        : base(firstStep)
    {
    }
}
