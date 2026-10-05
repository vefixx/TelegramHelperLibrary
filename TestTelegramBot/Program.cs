using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.BotAPI;

namespace TestTelegramBot;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = new HostApplicationBuilder(args);

        builder.Services.AddHostedService<UpdatesWorker>();

        var client = new TelegramBotClient("");
        builder.Services.AddSingleton<TelegramBotClient>(client);

        var app = builder.Build();
        await app.RunAsync();
    }
}