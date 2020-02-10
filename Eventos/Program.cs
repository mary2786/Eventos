using Eventos.Persistence;
using Eventos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Eventos
{
    public class Program
    {
        static void Main(string[] args)
        {
            DependencyContainer container = new DependencyContainer();
            IEventService eventService = container.GetServiceProvider().GetService<IEventService>();
            string path = @"c:\Temp\eventos.txt";
            eventService.PrintEvents(path);          
        }
    }
}