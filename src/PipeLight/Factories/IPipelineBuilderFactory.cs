using PipeLight.Builders;

namespace PipeLight.Factories;

public interface IPipelineBuilderFactory
{
    PipelineBuilder CreateBuilder();
}