using PipeLight.Abstractions.Pipes;

namespace PipeLight.Extensions;

public static class EnumerableExtensions
{
    public static PipesDictionary ToPipesDictionary(this IEnumerable<IPipeEnter> source)
    {
        if (source is null) throw new NullReferenceException(nameof(source));
        var dictionary = new PipesDictionary();
        foreach (var element in source)
            dictionary.Add(element.Id.ToString(), element);
        return dictionary;
    }
}