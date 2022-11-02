using PipeLight.Interfaces;

namespace PipeLight.Nodes.Interfaces;

public interface IPipelineEnter<TIn>
{
    Task PushAsync(TIn payload, IPipelineContext context);
}
