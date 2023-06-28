using System.Collections.ObjectModel;

namespace PipeLight.Abstractions.Pipes;

// public class PipesDictionary<T> : Dictionary<Guid, IPipeEnter<T>>
// {
// }
//
// public sealed class ReadOnlyPipesDictionary<T> : ReadOnlyDictionary<Guid, IPipeEnter<T>>
// {
//     public ReadOnlyPipesDictionary(PipesDictionary<T> dictionary) : base(dictionary)
//     {
//     }
// }

public class PipesDictionary : Dictionary<Guid, IPipeEnter>
{
}

public sealed class ReadOnlyPipesDictionary : ReadOnlyDictionary<Guid, IPipeEnter>
{
    public ReadOnlyPipesDictionary(PipesDictionary dictionary) : base(dictionary)
    {
    }
}
