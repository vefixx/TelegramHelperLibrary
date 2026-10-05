using System.Collections.Concurrent;
using System.Reflection;
using TelegramHelperLibrary.Exceptions;

namespace TelegramHelperLibrary;

public class ReflectionStorage
{
    private readonly ConcurrentDictionary<string, MethodInfo> _slashCommandHandlersStorage = new();

    public void AddToSlashCommandHandlersStorage(string command, MethodInfo methodInfo)
    {
        // если зарегистрировано два метода, которые обрабатывают одну и ту же команду
        if (!_slashCommandHandlersStorage.TryAdd(command, methodInfo))
        {
            throw new HandlerDuplicateException($"Найдена дубликация SlashCommandHandler. К команде \"{command}\" привязано два или более обработчика.");
        }
    }
}
