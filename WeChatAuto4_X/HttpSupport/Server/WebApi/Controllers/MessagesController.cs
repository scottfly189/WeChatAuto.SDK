using Microsoft.AspNetCore.Mvc;
using OneOf;
using Server.WebApi.Contracts.Requests;
using Server.WebApi.Services;
using WeChatAuto.Models;
using WeChatAuto.Options;

namespace Server.WebApi.Controllers;

/// <summary>
/// 消息管理：发送文本/文件/表情/语音、聊天历史、拍一拍、引用。
/// </summary>
[ApiController]
[Route("api/v1/clients/{clientId}/messages")]
public sealed class MessagesController : ControllerBase
{
    private readonly IWeChatClientProvider _provider;

    public MessagesController(IWeChatClientProvider provider)
    {
        _provider = provider;
    }

    [HttpGet("title")]
    public async Task<IActionResult> GetTitle(string clientId)
        => Ok(await _provider.Resolve(clientId).GetTitle());

    [HttpGet("only-title")]
    public async Task<IActionResult> GetOnlyTitle(string clientId)
        => Ok(await _provider.Resolve(clientId).GetOnlyTitle());

    [HttpPost("focus-input")]
    public async Task<IActionResult> FocusInput(string clientId)
    {
        await _provider.Resolve(clientId).FocuseSenderInput();
        return NoContent();
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send(string clientId, [FromBody] SendMessageRequest request)
    {
        var client = _provider.Resolve(clientId);

        OneOf<string, string[], List<string>> atUser = default;
        if (request.AtUsers is { Count: > 0 })
            atUser = request.AtUsers;

        if (string.IsNullOrWhiteSpace(request.Who))
            await client.SendMessage(request.Message, atUser, request.Refer);
        else
            await client.SendMessage(request.Who, request.Message, atUser, request.Refer);

        return NoContent();
    }

    [HttpPost("send-file")]
    public async Task<IActionResult> SendFile(string clientId, [FromBody] SendFileRequest request)
    {
        await _provider.Resolve(clientId).SendFile(request.Who ?? "", request.Files);
        return NoContent();
    }

    [HttpPost("send-emoji")]
    public async Task<IActionResult> SendEmoji(string clientId, [FromBody] SendEmojiRequest request)
    {
        OneOf<int, string> emoji;
        if (request.Index is int index)
            emoji = index;
        else if (!string.IsNullOrWhiteSpace(request.Emoji))
            emoji = request.Emoji;
        else
            return BadRequest(new ProblemDetails { Title = "参数错误", Detail = "emoji 与 index 至少提供一个。" });

        await _provider.Resolve(clientId).SendEmoji(request.Who ?? "", emoji, request.AtUsers);
        return NoContent();
    }

    [HttpPost("send-voice-chat")]
    public async Task<IActionResult> SendVoiceChat(string clientId, [FromBody] WhoRequest request)
    {
        await _provider.Resolve(clientId).SendVoiceChat(request.Who ?? "");
        return NoContent();
    }

    [HttpPost("send-video-chat")]
    public async Task<IActionResult> SendVideoChat(string clientId, [FromBody] WhoRequest request)
    {
        await _provider.Resolve(clientId).SendVedioChat(request.Who ?? "");
        return NoContent();
    }

    [HttpPost("send-voice-chats")]
    public async Task<IActionResult> SendVoiceChats(string clientId, [FromBody] SendVoiceChatsRequest request)
    {
        await _provider.Resolve(clientId).SendVoiceChats(request.Who ?? "", request.Partner);
        return NoContent();
    }

    [HttpPost("send-voice-message")]
    public async Task<IActionResult> SendVoiceMessage(string clientId, [FromBody] SendVoiceMessageRequest request)
    {
        var client = _provider.Resolve(clientId);
        if (string.IsNullOrWhiteSpace(request.Who))
            await client.SendVoiceMessage(request.FilePath);
        else
            await client.SendVoiceMessage(request.Who, request.FilePath);
        return NoContent();
    }

    [HttpPost("send-voice-message-tts")]
    public async Task<IActionResult> SendVoiceMessageTts(string clientId, [FromBody] SendVoiceMessageTtsRequest request)
    {
        await _provider.Resolve(clientId).SendVoiceMessageWithTTS(
            request.Who ?? "",
            request.ApiKey,
            request.Message,
            request.Options ?? new VoiceOptions(),
            request.OptimizeWithLlm);
        return NoContent();
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory(
        string clientId,
        [FromQuery] string? who,
        [FromQuery] DateTime? date,
        [FromQuery(Name = "start_date")] DateTime? startDate,
        [FromQuery(Name = "end_date")] DateTime? endDate)
    {
        var client = _provider.Resolve(clientId);

        List<ChatSimpleMessage> result;
        if (!string.IsNullOrWhiteSpace(who))
        {
            if (startDate.HasValue && endDate.HasValue)
                result = await client.GetChatHistory(who, startDate.Value, endDate.Value);
            else
                result = await client.GetChatHistory(who, date ?? default);
        }
        else
        {
            result = await client.GetChatHistory(date ?? default);
        }

        return Ok(result);
    }

    [HttpPost("tap")]
    public async Task<IActionResult> Tap(string clientId, [FromBody] TapRequest request)
        => Ok(await _provider.Resolve(clientId).TapWho(request.Who, request.PrevScrollNumber));

    [HttpPost("reference")]
    public async Task<IActionResult> Reference(string clientId, [FromBody] ReferenceRequest request)
    {
        var client = _provider.Resolve(clientId);
        bool ok = request.Message is not null
            ? await client.ReferencedMessage(request.Message, request.PrevScrollNumber)
            : await client.ReferencedMessage(request.Who ?? "", request.MessageContent ?? "", request.PrevScrollNumber);
        return Ok(ok);
    }

    [HttpPost("reference-last")]
    public async Task<IActionResult> ReferenceLast(string clientId)
        => Ok(await _provider.Resolve(clientId).ReferencedLastMessage());
}
