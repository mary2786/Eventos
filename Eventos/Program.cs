using Eventos.Persistence;
using Eventos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Eventos
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
           .AddSingleton<IEventRepository, EventRepository>()
           .AddSingleton<IFileWrapper, FileWrapper>()
           .AddSingleton<IPrintEvent, PrintEvent>()
           .AddSingleton<IEventValidator, EventValidator>()
           .AddScoped<ICurrentDate, CurrentDate>()
           .AddScoped<IDateConverter, DateConverter>()
           .AddScoped<ITimeInterval, TimeInterval>()
           .AddScoped<IDateEventUtil, DateEventUtil>()
           .AddScoped<IEventService, EventService>()
           .BuildServiceProvider();

            IEventService eventService = serviceProvider.GetService<IEventService>();
            string path = @"c:\Temp\eventos.txt";
            eventService.PrintEvents(path);          
        }
    }
}