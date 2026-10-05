namespace TelegramHelperLibrary.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class MessageHandlerAttribute : Attribute
{
    public MessageHandlerAttribute(string content)
    {
        Content = content;
    }

    public string Content { get; }
}
