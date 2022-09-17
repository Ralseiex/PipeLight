namespace PipeLight.Interfaces;

public interface IPipelineStepExit<TOut>
{
    IPipelineStepEnter<TOut> NextStep { get; set; }
}