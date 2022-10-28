namespace PipeLight.Interfaces.Steps;

public interface IPipelineStepExit<TOut>
{
    IPipelineStepEnter<TOut> NextStep { get; set; }
}