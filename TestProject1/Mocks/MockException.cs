namespace TestProject1.Mocks;

[Serializable]
public class MockException : Exception
{
    public MockException() { }
    public MockException(string message) : base(message) { }
    public MockException(string message, Exception inner) : base(message, inner) { }
    protected MockException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
