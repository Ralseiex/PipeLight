namespace PipeLight.Abstractions.Pipes;

public interface IActionPipe<T> : IPipeEnter<T>, IPipeExit<T>
{
}
