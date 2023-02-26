namespace PipeLight.Abstractions.Pipes;

public interface IPipeExit<TOut>
{
    IPipeEnter<TOut>? NextPipe { get; set; }
}