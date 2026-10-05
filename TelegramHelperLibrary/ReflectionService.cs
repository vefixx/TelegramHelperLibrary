using System.Reflection;
using TelegramHelperLibrary.Attributes;

namespace TelegramHelperLibrary;

public class ReflectionService
{
    private static readonly Assembly s_assembly = Assembly.GetEntryAssembly();
    private readonly ReflectionStorage _reflectionStorage;

    public ReflectionService(ReflectionStorage reflectionStorage)
    {
        _reflectionStorage = reflectionStorage;
    }

    public void RegisterAllHandlers()
    {
        RegisterSlashCommandHandlers();
        RegisterMessageHandlers();
    }

    /// <summary>
    /// Ищет и кеширует в текущей сборке методы-обработчики, которые имеют атрибут <see cref="SlashCommandHandlerAttribute"/>
    /// </summary>
    private void RegisterSlashCommandHandlers()
    {
        var slashCommandHandlerMethods = s_assembly.GetTypes()
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttributes(typeof(SlashCommandHandlerAttribute), false).Length > 0)
            .ToArray();

        foreach (MethodInfo methodInfo in slashCommandHandlerMethods)
        {
            var attribute =
                methodInfo.GetCustomAttribute<SlashCommandHandlerAttribute>();

            _reflectionStorage.AddToSlashCommandHandlersStorage(attribute.Command, methodInfo);
        }
    }


    /// <summary>
    /// Ищет и кеширует в текущей сборке методы-обработчики, которые имеют атрибут <see cref="MessageHandlerAttribute"/>
    /// </summary>
    private void RegisterMessageHandlers()
    {
        var messageHandlerMethods = s_assembly.GetTypes()
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttributes(typeof(MessageHandlerAttribute), false).Length > 0)
            .ToArray();

        foreach (MethodInfo methodInfo in messageHandlerMethods)
        {
            var attribute =
                methodInfo.GetCustomAttribute<MessageHandlerAttribute>();

            // TODO: Создать в классе хранилища метод, который кеширует обработчики сообщений
        }
    }
}
