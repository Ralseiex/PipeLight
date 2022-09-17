namespace PipeLight.Interfaces;

public interface IPipelineStepEnter<TIn>
{
    Task PushAsync(TIn data, IPipelineContext context);
}
