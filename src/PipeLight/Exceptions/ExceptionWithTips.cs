namespace PipeLight.Exceptions;

public class ExceptionWithTips : Exception
{
    private readonly List<string> _tips = new();
    
    public ExceptionWithTips()
    {
    }

    public ExceptionWithTips(string message, params string[] tips) : base(message)
    {
        _tips.AddRange(tips);
    }

    public ExceptionWithTips(string message, Exception inner, params string[] tips) : base(message, inner)
    {
        _tips.AddRange(tips);
    }

    public string[] Tips => _tips.ToArray();
}