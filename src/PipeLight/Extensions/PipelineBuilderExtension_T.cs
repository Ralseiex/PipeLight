using PipeLight.Abstractions.Builders;
using PipeLight.Steps;

namespace PipeLight.Extensions;

public static partial class PipelineBuilderExtension
{
    public static IPipelineBuilder<T> AddStep<T>(this IPipelineBuilder<T> builder, Func<T, T> stepHandler)
        => builder.AddStep(new FuncPipeStep<T>(stepHandler));
    
    public static IPipelineBuilder<TIn, TOut> AddTransform<TIn, TOut>(this IPipelineBuilder<TIn> builder, Func<TIn, TOut> stepHandler)
        => builder.AddTransform(new FuncTransform<TIn, TOut>(stepHandler));

    public static ISealedPipelineBuilder<T> Seal<T>(this IPipelineBuilder<T> builder, Action<T> sealHandler)
        => builder.Seal(new ActionSealedStep<T>(sealHandler));
}