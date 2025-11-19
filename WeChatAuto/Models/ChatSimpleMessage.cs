namespace WeChatAuto.Models
{
    public class ChatSimpleMessage
    {
        public string Who { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return $"{Who}: {Message}";
        }
    }
}