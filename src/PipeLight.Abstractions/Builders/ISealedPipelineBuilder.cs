using PipeLight.Abstractions.Pipelines;

namespace PipeLight.Abstractions.Builders;

public interface ISealedPipelineBuilder<in T>
{
    int PipelineLength { get; }
    ISealedPipeline<T> Build();
}
