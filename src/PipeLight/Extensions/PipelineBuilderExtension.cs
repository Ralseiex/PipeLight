using PipeLight.Abstractions.Builders;
using PipeLight.Steps;

namespace PipeLight.Extensions;

public static partial class PipelineBuilderExtension
{
    public static IPipelineBuilder<T> AddStep<T>(this IPipelineBuilder builder, Func<T, T> stepHandler)
        => builder.AddAction(new PipelineAction<T>(stepHandler));

    public static IPipelineBuilder<TIn, TOut> AddTransform<TIn, TOut>(this IPipelineBuilder builder, Func<TIn, TOut> stepHandler)
        => builder.AddTransform(new PipelineTransformStep<TIn, TOut>(stepHandler));

    public static ISealedPipelineBuilder<T> Seal<T>(this IPipelineBuilder builder, Action<T> sealHandler)
        => builder.Seal(new PipelineSeal<T>(sealHandler));
}