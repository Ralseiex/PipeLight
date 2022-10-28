namespace PipeLight.Interfaces.Steps;

public interface IPipelineStep<TIn> : IPipelineStepEnter<TIn> { }
public interface IPipelineStep<TIn, TOut> : IPipelineStepEnter<TIn>, IPipelineStepExit<TOut> { }
