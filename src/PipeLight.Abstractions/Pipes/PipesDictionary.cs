using System.Collections.ObjectModel;

namespace PipeLight.Abstractions.Pipes;

public class PipesDictionary : Dictionary<string, IPipeEnter>
{
}

public sealed class ReadOnlyPipesDictionary : ReadOnlyDictionary<string, IPipeEnter>
{
    public ReadOnlyPipesDictionary(PipesDictionary dictionary) : base(dictionary)
    {
    }
}