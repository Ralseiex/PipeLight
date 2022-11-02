namespace PipeLight.Nodes.Steps.Interfaces;

public interface IPipeTransform<TIn, TOut>
{
    Task<TOut> TransformAsync(TIn payload);
}
