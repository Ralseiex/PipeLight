using PipeLight.Abstractions.Context;

namespace PipeLight.Abstractions.Pipes;

public interface IPipeEnter
{
    Guid Id { get; }
    Task Push(object payload, IPipelineContext context);
}
public interface IPipeEnter<in TIn> : IPipeEnter
{
    Task Push(TIn payload, IPipelineContext context);
}
