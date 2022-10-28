namespace PipeLight.Steps.Interfaces;

public interface IPipeFitting<TIn, TOut>
{
    Task<TOut> FitAsync(TIn payload);
}
