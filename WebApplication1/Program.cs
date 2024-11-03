using Microsoft.AspNetCore.SignalR;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.MapHub<CurrencyHub>("/currencyHub");
            StartCurrencyUpdates(app.Services);


            app.Run();
        }


        private static void StartCurrencyUpdates(IServiceProvider services)
        {
            var random = new Random();
            var hubContext = services.GetService<IHubContext<CurrencyHub>>();

            new Thread(() =>
            {
                while (true)
                {
                    var usdToEur = Math.Round(random.NextDouble() * (1.2 - 1.0) + 1.0, 4);
                    var gbpToEur = Math.Round(random.NextDouble() * (1.2 - 1.0) + 1.0, 4);

                    //отправка обновлений
                    hubContext.Clients.All.SendAsync("ReceiveCurrencyUpdate", "USD/EUR", usdToEur);
                    hubContext.Clients.All.SendAsync("ReceiveCurrencyUpdate", "GDR/EUR", gbpToEur);

                    Thread.Sleep(5000);
                }
            }).Start();
        }
    }
}
