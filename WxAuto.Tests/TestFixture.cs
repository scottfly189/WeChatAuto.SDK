using Xunit;
using Xunit.Abstractions;

public class TestFixture : IDisposable
{
    public TestFixture()
    {
        Console.WriteLine("TestFixture constructor");
    }
    
    public void Dispose()
    {
        Console.WriteLine("TestFixture Dispose");
    }
}