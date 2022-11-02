namespace PipeLight.Pipes.Interfaces;

public interface ISealedPipe<T>
{
    Task PushAsync(T payload);
}