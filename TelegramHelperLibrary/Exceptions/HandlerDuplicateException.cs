namespace TelegramHelperLibrary.Exceptions;

public class HandlerDuplicateException : Exception
{
    public HandlerDuplicateException(string message) : base(message)
    {

    }
}
