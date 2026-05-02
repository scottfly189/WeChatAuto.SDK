

using WeChatAuto.Utils;
using Xunit.Abstractions;

namespace WeChatAuto.Tests.Utils;

public class ClipboardApiTests
{

    [Fact(DisplayName = "测试复制文件到剪贴板")]
    public void Test_CopyFilesToClipboard()
    {
        var result = ClipboardApi.CopyFilesToClipboard([@"C:\Users\Administrator\Desktop\ssss\1.webp",
            @"C:\Users\Administrator\Desktop\ssss\logo.png",
        @"C:\Users\Administrator\Desktop\ssss\3.pdf"]);
        Assert.True(result);
    }
}