

class WeChatAutoWebSocketServerExcpetion : Exception
{
    public string Request_Id { get; }
    public WeChatAutoWebSocketServerExcpetion(string message, string request_id) : base(message)
    {
        this.Request_Id = request_id;
    }
}