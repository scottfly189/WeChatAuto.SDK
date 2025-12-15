namespace WeChatAuto.MCP.Utils;
public static class Alert
{
    /// <summary>
    /// 显示提示信息
    /// </summary>
    /// <param name="message">提示信息</param>
    public static void Show(string message)
    {
        Console.Error.WriteLine(message);
    }
}