using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

using var client = new HttpClient();
var whoMessageList = new List<AutomationMessage>();
var whoFileList = new List<AutomationFile>();

////1. 测试发送文本消息接口: get /api/v1/message
var textMessage = new AutomationMessage()
{
    From = "Alex",
    To = "AI.Net",
    Message = "这是通过自动化\r\n发送的文本消息",  //如果要折行，需要在文本中包含\r\n，自动化会识别并正确发送成多行消息。
    MessageId = Guid.NewGuid().ToString() // 可选，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
};

whoMessageList.Add(textMessage);
for (int i = 1; i <= 8; i++)
{
    textMessage = new AutomationMessage()
    {
        From = "Alex",
        To = $"测试0{i}",
        Message = "这是通过自动化\r\n发送的文本消息",
    };
    whoMessageList.Add(textMessage);
}

whoMessageList.Add(textMessage);
whoMessageList.Add(textMessage);

//直接发送，不用等候，风控交给自动化UI运控制，如果需要返回，则要加上MessgageId
foreach (var item in whoMessageList)
{
    await client.GetAsync($"http://localhost:5000/api/v1/message?from={item.From}&to={item.To}&message={item.Message}&messageId={item.MessageId}");
}

//2. 测试发送文本消息接口: post /api/v1/message
textMessage = new AutomationMessage()
{
    From = "Alex",
    To = "AI.Net",
    Message = "这是通过自动化\r\n发送的文本消息",  //如果要折行，需要在文本中包含\r\n，自动化会识别并正确发送成多行消息。
    MessageId = Guid.NewGuid().ToString() // 可选，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
};

whoMessageList.Add(textMessage);
for (int i = 1; i <= 8; i++)
{
    textMessage = new AutomationMessage()
    {
        From = "Alex",
        To = $"测试0{i}",
        Message = "这是通过自动化\r\n发送的文本消息",
    };
    whoMessageList.Add(textMessage);
}

whoMessageList.Add(textMessage);
whoMessageList.Add(textMessage);
////直接发送，不用等候，风控交给自动化UI运控制，如果需要返回，则要加上MessgageId
foreach (var item in whoMessageList)
{
    HttpContent httpContent = JsonContent.Create(item);
    await client.PostAsync($"http://localhost:5000/api/v1/message", httpContent);
}


//3.测试发送图片、视频消息接口: post/api/v1/file
byte[] data = File.ReadAllBytes(@"..\test_resource\1.png");
var fileMessage = new AutomationFile()
{
    From = "Alex",
    To = "AI.Net",
    FileContent = data,
    ClientSuggestionFileName="1.png",
    MessageId = Guid.NewGuid().ToString() // 可选，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
};
whoFileList.Add(fileMessage);
data = File.ReadAllBytes(@"..\test_resource\2.md");
fileMessage = new AutomationFile()
{
    From = "Alex",
    To = "测试01",
    FileContent = data,
    ClientSuggestionFileName = "2.md",
    MessageId = Guid.NewGuid().ToString() // 可选，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
};
whoFileList.Add(fileMessage);
data = File.ReadAllBytes(@"..\test_resource\3.txt");
fileMessage = new AutomationFile()
{
    From = "Alex",
    To = "测试03",
    FileContent = data,
    ClientSuggestionFileName = "3.txt",
    MessageId = Guid.NewGuid().ToString() // 可选，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
};
whoFileList.Add(fileMessage);
data = File.ReadAllBytes(@"..\test_resource\4.mp4");
fileMessage = new AutomationFile()
{
    From = "Alex",
    To = "测试04",
    FileContent = data,
    ClientSuggestionFileName = "4.mp4",
    MessageId = Guid.NewGuid().ToString() // 可选，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
};
whoFileList.Add(fileMessage);
data = File.ReadAllBytes(@"..\test_resource\5.docx");
fileMessage = new AutomationFile()
{
    From = "Alex",
    To = "测试05",
    FileContent = data,
    ClientSuggestionFileName = "5.docx",
    MessageId = Guid.NewGuid().ToString() // 可选，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
};
whoFileList.Add(fileMessage);

for (int i = 1; i <= 8; i++)
{
    data = File.ReadAllBytes(@"..\test_resource\1.png");
    fileMessage = new AutomationFile()
    {
        From = "Alex",
        To = $"测试0{i}",
        FileContent = data,
        ClientSuggestionFileName = "1.png",
        MessageId = Guid.NewGuid().ToString() // 可选，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
    };
    whoFileList.Add(fileMessage);
}

//直接发送，不用等候，风控交给自动化UI运控制，如果需要返回，则要加上MessgageId
foreach (var item in whoFileList)
{
    HttpContent httpContent = JsonContent.Create(item);
    await client.PostAsync($"http://localhost:5000/api/v1/file", httpContent);
}


await Task.Delay(-1);



//--------------------------- 下面是DTO模型类 ---------------------------


/// <summary>
/// 文字消息
/// </summary>
public class AutomationMessage
{
    /// <summary>
    /// 通过谁（自己微信的昵称）发送
    /// </summary>
    [JsonPropertyName("from")]
    public required string From { get; set; }
    /// <summary>
    /// 发送给谁 - 微信好友/群的昵称
    /// </summary>
    [JsonPropertyName("to")]
    public required string To { get; set; }
    /// <summary>
    /// 文字消息
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; set; }
    /// <summary>
    /// message_id - 可选项，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
    /// </summary>
    [JsonPropertyName("message_id")]
    public string? MessageId { get; set; }

}



/// <summary>
/// 发送图片、文件等的模型类
/// </summary>
public class AutomationFile
{
    /// <summary>
    /// 通过谁（自己微信的昵称）发送
    /// </summary>
    [JsonPropertyName("from")]
    public required string From { get; set; }
    /// <summary>
    /// 发送给谁 - 微信好友/群的昵称
    /// </summary>
    [JsonPropertyName("to")]
    public required string To { get; set; }
    /// <summary>
    /// 图片、视频、文件转成的Base64内容
    /// </summary>
    [JsonPropertyName("file_content")]
    public required byte[] FileContent { get; set; }
    /// <summary>
    /// 建议客户端名称,包括后缀名，如: 三月计划.png，三月计划.xlsx
    /// </summary>
    [JsonPropertyName("client_suggestion_file_name")]
    public required string ClientSuggestionFileName { get; set; }
    /// <summary>
    /// message_id - 可选项，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
    /// </summary>
    [JsonPropertyName("message_id")]
    public string? MessageId { get; set; }
}