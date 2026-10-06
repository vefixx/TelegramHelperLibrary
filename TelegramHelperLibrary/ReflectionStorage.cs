using System.Collections.Concurrent;
using System.Reflection;
using TelegramHelperLibrary.Exceptions;

namespace TelegramHelperLibrary;

public class ReflectionStorage
{
    private readonly ConcurrentDictionary<string, MethodInfo> _slashCommandHandlersStorage = new();
    private readonly ConcurrentDictionary<string, MethodInfo> _messageHandlersStorage = new();

    public void AddToSlashCommandHandlersStorage(string command, MethodInfo methodInfo)
    {
        // если зарегистрировано два метода, которые обрабатывают одну и ту же команду
        if (!_slashCommandHandlersStorage.TryAdd(command, methodInfo))
        {
            throw new HandlerDuplicateException($"К команде \"{command}\" привязано два или более обработчика.");
        }
    }

    public void AddToMessageHandlersStorage(string content, MethodInfo methodInfo)
    {
        if (!_messageHandlersStorage.TryAdd(content, methodInfo))
        {
            throw new HandlerDuplicateException($"К контенту \"{content}\" привязано два или более обработчика.");
        }
    }
}
