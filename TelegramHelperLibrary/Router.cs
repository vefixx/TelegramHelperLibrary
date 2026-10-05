using Microsoft.Extensions.Hosting;
using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace TelegramHelperLibrary;


/*
 * Запускает цикл лонг-поллинга, получает события, фильтрует, ищет обработчики
 */
public class Router : IHostedService
{
    private readonly TelegramBotClient _client;

    public Router(TelegramBotClient client)
    {
        _client = client;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var updates = await _client.GetUpdatesAsync(cancellationToken: cancellationToken);

        if (updates.Any())
        {
            updates = await _client.GetUpdatesAsync(updates.Last().UpdateId + 1, cancellationToken: cancellationToken);
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (updates.Any())
                {
                    updates = await _client.GetUpdatesAsync(updates.Last().UpdateId + 1, cancellationToken: cancellationToken);
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}
