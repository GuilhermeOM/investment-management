namespace InvestmentManagement.WS.Events.Exceptions;

public class EventFailedException(string message = "Falha ao executar o evento.") : Exception(message) { }
