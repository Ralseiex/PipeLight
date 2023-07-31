namespace PipeLight.Mocks;

public class MockPayloadInt
{
    public int Value { get; set; }
    public object? RefValue { get; set; }

    public MockPayloadInt Clone() => new()
    {
        Value = Value,
        RefValue = RefValue
    };
}


public class MockPayloadString
{
    public string? Value { get; set; }
    public object? RefValue { get; set; }
    
    public MockPayloadString Clone() => new()
    {
        Value = Value,
        RefValue = RefValue
    };
}
