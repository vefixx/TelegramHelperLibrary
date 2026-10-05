using TelegramHelperLibrary;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        var reflectionService = new ReflectionService(new ReflectionStorage());
        reflectionService.RegisterAllHandlers();
    }
}