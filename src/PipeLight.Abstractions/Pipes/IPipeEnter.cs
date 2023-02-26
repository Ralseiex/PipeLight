using PipeLight.Abstractions.Context;

namespace PipeLight.Abstractions.Pipes;

public interface IPipeEnter<in TIn>
{
    Task Push(TIn payload, IPipelineContext context);
}
