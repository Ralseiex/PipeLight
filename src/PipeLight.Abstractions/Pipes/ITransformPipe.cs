namespace PipeLight.Abstractions.Pipes;

public interface ITransformPipe<in TIn, TOut> : IPipeEnter<TIn>, IPipeExit<TOut>
{
}
