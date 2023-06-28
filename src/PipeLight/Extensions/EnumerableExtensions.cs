using PipeLight.Abstractions.Pipes;

namespace PipeLight.Extensions;

public static class EnumerableExtensions
{
    // public static PipesDictionary<T> ToPipesDictionary<T>(this IEnumerable<IPipeEnter<T>> source)
    // {
    //     if (source == null) throw new ArgumentException("source");
    //     var d = new PipesDictionary<T>();
    //     foreach (var element in source) 
    //         d.Add(element.Id, element);
    //     return d;
    // }
    
    public static PipesDictionary ToPipesDictionary(this IEnumerable<IPipeEnter> source)
    {
        if (source == null) throw new ArgumentException("source");
        var d = new PipesDictionary();
        foreach (var element in source) 
            d.Add(element.Id, element);
        return d;
    }
}
