using TestProject1.Mocks;

namespace TestProject1;

internal class TestHelper
{
    public static MockPayload GetMockPayload(int value = 5, object refValue = null)
    {
        if (refValue is null)
            refValue = new();
        return new()
        {
            Value = value,
            RefValue = refValue,
        };
    }
}
