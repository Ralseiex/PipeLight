namespace PipeLight.Interfaces;

public interface ISealedPipeline<TIn>
{
    Task PushAsync(TIn payload);
}