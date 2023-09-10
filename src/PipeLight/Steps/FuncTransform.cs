using PipeLight.Abstractions.Steps;

namespace PipeLight.Steps;

internal sealed class PipelineTransformStep<TIn, TOut> : IPipelineTransform<TIn, TOut>
{
    private readonly Func<TIn, TOut> _handler;

    public PipelineTransformStep(Func<TIn, TOut> transformHandler) => _handler = transformHandler;

    public async Task<TOut> Transform(TIn payload) => await Task.FromResult(_handler(payload));
}