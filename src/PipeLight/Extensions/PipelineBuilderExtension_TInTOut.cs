using PipeLight.Abstractions.Builders;
using PipeLight.Steps;

namespace PipeLight.Extensions;

public static partial class PipelineBuilderExtension
{
    public static IPipelineBuilder<TIn, TOut> AddStep<TIn, TOut>(this IPipelineBuilder<TIn, TOut> builder, Func<TOut, TOut> stepHandler)
        => builder.AddStep(new FuncPipeStep<TOut>(stepHandler));

    public static IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TOut, TNewOut>(this IPipelineBuilder<TIn, TOut> builder, Func<TOut, TNewOut> stepHandler)
        => builder.AddTransform(new FuncTransform<TOut, TNewOut>(stepHandler));

    public static ISealedPipelineBuilder<TIn> Seal<TIn, TOut>(this IPipelineBuilder<TIn, TOut> builder, Action<TOut> sealHandler)
        => builder.Seal(new ActionSealedStep<TOut>(sealHandler));
}