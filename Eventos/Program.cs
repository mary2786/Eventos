using Eventos.Persistencia;
using Eventos.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace Eventos
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
           .AddSingleton<IEventRepository, EventRepository>()
           .AddScoped<IEventService, EventService>()
           .BuildServiceProvider();

            IEventService eventService = serviceProvider.GetService<IEventService>();

            foreach (KeyValuePair<string, string> keyValue in eventService.GetEvents())
            {
                Console.WriteLine("\t" + keyValue.Key + " " + keyValue.Value);
            }
        }
    }
}
