namespace PipeLight.Interfaces;

public interface IPipelineStep<TIn, TOut> : IPipelineStepEnter<TIn>, IPipelineStepExit<TOut> { }
