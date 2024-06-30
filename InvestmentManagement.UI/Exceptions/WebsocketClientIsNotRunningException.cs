namespace InvestmentManagement.UI.Exceptions;

public class WebsocketClientIsNotRunningException(string message = "Não foi possível conectar com o servidor websocket.") : Exception(message) { }
