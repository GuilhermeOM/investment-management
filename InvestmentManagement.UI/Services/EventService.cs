namespace InvestmentManagement.UI.Services;

using System;
using InvestmentManagement.UI.Entities.Enums;

public static class EventService
{
    public static void AskForCommands()
    {
        Console.WriteLine("===================================================");
        Console.WriteLine("");

        Console.WriteLine("LISTA DE COMANDOS:");
        Console.WriteLine("0. Logar");
        Console.WriteLine("1. Logoff");
        Console.WriteLine("2. Cadastrar produto financeiro");
        Console.WriteLine("3. Consultar produtos financeiros");
        Console.WriteLine("4. Comprar produto financeiro");
        Console.WriteLine("5. Vender produto financeiro");
        Console.WriteLine("6. Extrato");

        Console.WriteLine("");
        Console.WriteLine("===================================================");
        Console.WriteLine("");

        Console.Write("Caso deseje executar algum, digite o número: ");

        var didParse = int.TryParse(Console.ReadLine(), out var commandId);

        if (!didParse)
        {
            Console.WriteLine("Input entrado não é reconhecido como um comando interno, por favor, tente novamente.");
            AskForCommands();
        }

        FilterCommandById(commandId);
    }

    private static void FilterCommandById(int commandId)
    {
        var command = (Command)commandId;

        switch (command)
        {
            case Command.LOGIN:
                Console.WriteLine("LOGIN");
                break;
            case Command.LOGOFF:
                Console.WriteLine("LOGOFF");
                break;
            case Command.REGISTER:
                Console.WriteLine("REGISTER");
                break;
            case Command.CONSULT:
                Console.WriteLine("CONSULT");
                break;
            case Command.BUY:
                Console.WriteLine("BUY");
                break;
            case Command.SELL:
                Console.WriteLine("SELL");
                break;
            case Command.EXTRACT:
                Console.WriteLine("EXTRACT");
                break;
            default:
                Console.WriteLine("Input entrado não é reconhecido como um comando interno, por favor, tente novamente.");
                break;
        }

        AskForCommands();
    }
}
