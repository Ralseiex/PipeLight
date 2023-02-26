using PipeLight.Builders;
using PipeLight.Steps;

namespace PipeLight.Extensions;

public static partial class PipelineBuilderExtension
{
    public static PipelineBuilder<T> AddStep<T>(this PipelineBuilder<T> builder, Func<T, T> stepHandler)
        => builder.AddStep(new FuncPipeStep<T>(stepHandler));
    
    public static PipelineBuilder<TIn, TOut> AddTransform<TIn, TOut>(this PipelineBuilder<TIn> builder, Func<TIn, TOut> stepHandler)
        => builder.AddTransform(new FuncTransform<TIn, TOut>(stepHandler));

    public static SealedPipelineBuilder<T> Seal<T>(this PipelineBuilder<T> builder, Action<T> sealHandler)
        => builder.Seal(new ActionSealedStep<T>(sealHandler));
}