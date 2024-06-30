namespace InvestmentManagement.UI;

using InvestmentManagement.UI.Services;

public class Worker(ISocketService socketService) : BackgroundService
{
    private readonly ISocketService socketService = socketService;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // ASCII art
        Console.WriteLine("\r\n\r\n  _____                     _                        _                                                            _   \r\n  \\_   \\_ ____   _____  ___| |_ _ __ ___   ___ _ __ | |_  /\\/\\   __ _ _ __   __ _  __ _  ___ _ __ ___   ___ _ __ | |_ \r\n   / /\\/ '_ \\ \\ / / _ \\/ __| __| '_ ` _ \\ / _ \\ '_ \\| __|/    \\ / _` | '_ \\ / _` |/ _` |/ _ \\ '_ ` _ \\ / _ \\ '_ \\| __|\r\n/\\/ /_ | | | \\ V /  __/\\__ \\ |_| | | | | |  __/ | | | |_/ /\\/\\ \\ (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_ \r\n\\____/ |_| |_|\\_/ \\___||___/\\__|_| |_| |_|\\___|_| |_|\\__\\/    \\/\\__,_|_| |_|\\__,_|\\__, |\\___|_| |_| |_|\\___|_| |_|\\__|\r\n                                                                                  |___/                               \r\n\r\n");

        this.socketService.OpenSocketConnection();

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        this.socketService.CloseSocketConnection();
    }
}
