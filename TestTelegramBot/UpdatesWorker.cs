using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace TestTelegramBot;

public class UpdatesWorker : IHostedService
{
    private readonly ILogger<UpdatesWorker> _logger;
    private readonly TelegramBotClient _client;
    
    public UpdatesWorker(ILogger<UpdatesWorker> logger, TelegramBotClient client)
    {
        _logger = logger;
        _client = client;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начинаем слушать события");
        
        var updates = await _client.GetUpdatesAsync(cancellationToken: cancellationToken);
            
        if (updates.Any())
            updates = await _client.GetUpdatesAsync(updates.Last().UpdateId + 1, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (updates.Any())
                {
                    _logger.LogInformation("Новое обновление, count={1}", updates.Count());
                    updates = await _client.GetUpdatesAsync(offset: updates.First().UpdateId + 1, cancellationToken: cancellationToken);
                }
                else
                {
                    updates = await _client.GetUpdatesAsync(cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка при слушании событий");
                await Task.Delay(10_000, cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}