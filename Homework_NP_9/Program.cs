using Microsoft.AspNetCore.SignalR.Client;

namespace Homework_NP_9
{
    internal class Program
    {
        static async Task Main()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7272/currencyHub")
                .Build();


            connection.On<string, decimal>("ReceiveCurrencyUpdate", (currencyPair, rate) =>
            {
                Console.WriteLine($"New rate for {currencyPair}: {rate}");
            });

            await connection.StartAsync();
            Console.WriteLine("Connected to server.");
            Console.ReadLine();

        }
    }
}
