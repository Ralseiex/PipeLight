namespace PipeLight.Tests.Mocks;

[Serializable]
public class MockException : Exception
{
    public MockException() { }
    public MockException(string message) : base(message) { }
    public MockException(string message, Exception inner) : base(message, inner) { }
}
