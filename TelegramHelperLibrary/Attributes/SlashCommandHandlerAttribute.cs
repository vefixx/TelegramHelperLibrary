namespace TelegramHelperLibrary.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class SlashCommandHandlerAttribute : Attribute
{
    public SlashCommandHandlerAttribute(string command)
    {
        Command = command;
    }

    public string Command { get; }
}
