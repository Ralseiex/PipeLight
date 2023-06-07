using PipeLight.Abstractions.Context;

namespace PipeLight.Abstractions.Pipes;

public interface IPipeEnter<in TIn>
{
    Guid Id { get; }
    Task Push(TIn payload, IPipelineContext context);
}
