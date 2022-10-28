namespace PipeLight.Interfaces.Steps;

public interface IPipelineStepEnter<TIn>
{
    Task PushAsync(TIn payload, IPipelineContext context);
}
