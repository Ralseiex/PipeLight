// using PipeLight.Abstractions.Builders;
// using PipeLight.Abstractions.Pipelines;
// using PipeLight.Builders;
// using PipeLight.Steps;
//
// namespace PipeLight.Pipelines;
//
// public abstract class PipelineBase<TIn, TOut> : IPipeline<TIn, TOut>
// {
//     private readonly IPipeline<TIn, TOut>? _innerPipeline;
//
//     protected abstract IPipelineBuilder<TIn, TOut> Configure(IPipelineBuilder<TIn> builder);
//
//     public async Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default)
//     {
//         if (_innerPipeline is null)
//         {
//             
//             _innerPipeline = Configure();
//         }
//         return await _innerPipeline.Push(payload, cancellationToken);
//     }
// }
//
// public abstract class PipelineBase<T> : IPipeline<T>
// {
//     private readonly IPipeline<T> _innerPipeline;
//
//     
//     protected PipelineBase()
//     {
//         var builder = new PipelineBuilder<T>(new ActivatorStepResolver());
//         _innerPipeline = Configure(builder).Build();
//     }
//     
//     protected abstract IPipelineBuilder<T> Configure(IPipelineBuilder<T> builder);
//
//     public Task<T> Push(T payload, CancellationToken cancellationToken = default)
//     {
//         return _innerPipeline.Push(payload, cancellationToken);
//     }
// }
