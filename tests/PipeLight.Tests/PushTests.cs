using PipeLight.Abstractions.Builders;
using PipeLight.Extensions;
using PipeLight.Pipelines;

namespace PipeLight.Tests;

public class PushTests
{
}

public class MyPipeline : PipelineBase<string>
{
    public override void ConfigurePipeline(IPipelineBuilder<string> builder)
    {
        builder.AddStep(x => x)
            .AddTransform(x => int.Parse(x))
            .AddStep(x=>x);
    }
}