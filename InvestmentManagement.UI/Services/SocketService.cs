namespace InvestmentManagement.UI.Services;

using InvestmentManagement.UI.Exceptions;
using Websocket.Client;

public class SocketService : ISocketService
{
    public WebsocketClient? SocketClient { get; private set; }

    public async void OpenSocketConnection()
    {
        this.SocketClient ??= new WebsocketClient(new Uri("ws://127.0.0.1:8181"));

        await this.SocketClient.Start();

        if (!this.SocketClient.IsRunning)
        {
            throw new WebsocketClientIsNotRunningException();
        }
    }

    public void CloseSocketConnection()
    {
        if (this.SocketClient != null && this.SocketClient.IsRunning)
        {
            this.SocketClient.Dispose();
        }
    }
}
