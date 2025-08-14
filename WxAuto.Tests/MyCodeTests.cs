namespace WxAuto.Tests;

public class MyCodeTests
{
    [Fact]
    public void AddTest()
    {
        var myCode = new MyCode();
        var result = myCode.Add(1, 2);
        Assert.Equal(3, result);
    }
}
