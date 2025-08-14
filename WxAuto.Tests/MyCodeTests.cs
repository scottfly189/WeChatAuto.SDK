using Xunit;
using Xunit.Abstractions;

namespace WxAuto.Tests;

public class MyCodeTests: IClassFixture<TestFixture>
{
    private readonly ITestOutputHelper _output;
    private readonly TestFixture _fixture;
    public MyCodeTests(ITestOutputHelper output, TestFixture fixture)
    {
        _output = output;
        _output.WriteLine("MyCodeTests constructor");
        _fixture = fixture;
    }
    [Fact]
    public void AddTest()
    {
        var myCode = new MyCode();
        var result = myCode.Add(1, 2);
        Assert.Equal(3, result);
    }

    [Fact(DisplayName = "only test")]
    public void OnlyTest()
    {

    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(2, 3, 5)]
    [InlineData(3, 4, 7)]
    public void AddTest2(int a, int b, int expected)
    {
        var myCode = new MyCode();
        var result = myCode.Add(a, b);
        Assert.Equal(expected, result);
    }
}
