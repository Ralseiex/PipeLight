namespace PipeLight.Nodes.Interfaces;

public interface IPipelineExit<TOut>
{
    IPipelineEnter<TOut>? NextNode { get; set; }
}