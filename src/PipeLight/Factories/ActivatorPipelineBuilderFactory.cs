using PipeLight.Steps;

namespace PipeLight.Factories;

public sealed class ActivatorPipelineBuilderFactory : PipelineBuilderFactory
{
    public ActivatorPipelineBuilderFactory() : base(new ActivatorStepResolver())
    {
    }
}