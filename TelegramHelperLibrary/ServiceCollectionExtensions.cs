namespace TelegramHelperLibrary;


/*
 * Основная идея: сократить запуск бота до 1 строчки кода.
 * Пользователю не нужно прописывать самостоятельно создание клиента и создание лонг-поллинга.
 * Пользователь вызывает serives.AddTelegramBot();, передает настройки и
 *  этот метод уже сам создает клиент с регистрацией Router : IHosted в DI-контейнере
 */
public class ServiceCollectionExtensions
{

}
