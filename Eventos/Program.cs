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
           .AddScoped<ICurrentDateService, CurrentDateService>()
           .AddScoped<IDateEventUtil, DateEventUtil>()       
           .AddScoped<IEventService, EventService>()
           .BuildServiceProvider();

            IEventService eventService = serviceProvider.GetService<IEventService>();

            foreach (string @event in eventService.GetEvents())
            {
                string eventText =eventService.GetTextEvent(@event, new CultureInfo("es-MX"));
                Console.WriteLine("\t" + eventText);
            }
        }
    }
}