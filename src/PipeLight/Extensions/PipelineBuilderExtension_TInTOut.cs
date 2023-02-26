using PipeLight.Builders;
using PipeLight.Steps;

namespace PipeLight.Extensions;

public static partial class PipelineBuilderExtension
{
    public static PipelineBuilder<TIn, TOut> AddStep<TIn, TOut>(this PipelineBuilder<TIn, TOut> builder, Func<TOut, TOut> stepHandler)
        => builder.AddStep(new FuncPipeStep<TOut>(stepHandler));

    public static PipelineBuilder<TIn, TNewOut> AddTransform<TIn, TOut, TNewOut>(this PipelineBuilder<TIn, TOut> builder, Func<TOut, TNewOut> stepHandler)
        => builder.AddTransform(new FuncTransform<TOut, TNewOut>(stepHandler));

    public static SealedPipelineBuilder<TIn> Seal<TIn, TOut>(this PipelineBuilder<TIn, TOut> builder, Action<TOut> sealHandler)
        => builder.Seal(new ActionSealedStep<TOut>(sealHandler));
}