using Eventos.Persistencia;
using Eventos.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace Eventos
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
           .AddSingleton<IEventRepository, EventRepository>()
           .AddScoped<ICurrentDate, CurrentDate>()
           .AddScoped<IDateConverter, DateConverter>()
           .AddScoped<ITimeInterval, TimeInterval>()
           .AddScoped<IDateEventUtil, DateEventUtil>()
           .AddScoped<IEventService, EventService>()
           .BuildServiceProvider();

            IEventService eventService = serviceProvider.GetService<IEventService>();

            try
            {
                foreach (string @event in eventService.GetEvents())
                {
                    string eventText = eventService.GetTextEvent(@event, new CultureInfo("es-MX"));
                    Console.WriteLine("\t" + eventText);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}