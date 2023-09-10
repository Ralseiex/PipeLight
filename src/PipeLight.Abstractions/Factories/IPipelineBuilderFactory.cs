using PipeLight.Abstractions.Builders;

namespace PipeLight.Abstractions.Factories;

public interface IPipelineBuilderFactory
{
    IPipelineBuilder CreateBuilder();
    IPipelineBuilder<T> CreateBuilder<T>();
}