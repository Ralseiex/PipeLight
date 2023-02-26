using PipeLight.Builders;
using PipeLight.Steps;

namespace PipeLight.Extensions;

public static class PipelineBuilderExtension
{
    public static PipelineBuilder<T> AddStep<T>(this PipelineBuilder builder, Func<T, T> stepHandler)
    {
        var step = new FuncPipeStep<T>(stepHandler);
        return builder.AddStep(step);
    }
    public static PipelineBuilder<TIn, TOut> AddTransform<TIn, TOut>(this PipelineBuilder builder, Func<TIn, TOut> stepHandler)
    {
        var step = new FuncTransformStep<TIn, TOut>(stepHandler);
        return builder.AddTransform(step);
    }
    public static SealedPipelineBuilder<T> Seal<T>(this PipelineBuilder builder, Action<T> sealHandler)
    {
        var sealedStep = new ActionSealedStep<T>(sealHandler);
        return builder.Seal(sealedStep);
    }
    
    
    
    public static PipelineBuilder<T> AddStep<T>(this PipelineBuilder<T> builder, Func<T, T> stepHandler)
    {
        var step = new FuncPipeStep<T>(stepHandler);
        return builder.AddStep(step);
    }
    public static PipelineBuilder<TIn, TOut> AddTransform<TIn, TOut>(this PipelineBuilder<TIn> builder, Func<TIn, TOut> stepHandler)
    {
        var step = new FuncTransformStep<TIn, TOut>(stepHandler);
        return builder.AddTransform(step);
    }
    public static SealedPipelineBuilder<T> Seal<T>(this PipelineBuilder<T> builder, Action<T> sealHandler)
    {
        var sealedStep = new ActionSealedStep<T>(sealHandler);
        return builder.Seal(sealedStep);
    }
    
    
    
    public static PipelineBuilder<TIn, TOut> AddStep<TIn, TOut>(this PipelineBuilder<TIn, TOut> builder, Func<TOut, TOut> stepHandler)
    {
        var step = new FuncPipeStep<TOut>(stepHandler);
        return builder.AddStep(step);
    }
    public static PipelineBuilder<TIn, TNewOut> AddTransform<TIn, TOut, TNewOut>(this PipelineBuilder<TIn, TOut> builder, Func<TOut, TNewOut> stepHandler)
    {
        var step = new FuncTransformStep<TOut, TNewOut>(stepHandler);
        return builder.AddTransform(step);
    }
    public static SealedPipelineBuilder<TIn> Seal<TIn, TOut>(this PipelineBuilder<TIn, TOut> builder, Action<TOut> sealHandler)
    {
        var sealedStep = new ActionSealedStep<TOut>(sealHandler);
        return builder.Seal(sealedStep);
    }
}