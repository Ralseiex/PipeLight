using PipeLight.Abstractions.Pipelines;

namespace PipeLight.Abstractions.Builders;

public interface ISealedPipelineBuilder<in T>
{
    ISealedPipeline<T> Build();
}
