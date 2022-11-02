namespace PipeLight.Pipes.Interfaces;

public interface IPipeTransform<TIn, TOut>
{
    Task<TOut> TransformAsync(TIn payload);
}
