using Eventos.Persistence;
using Eventos.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Eventos
{
    public class DependencyContainer
    {
        public IServiceProvider GetServiceProvider()
        {
            IServiceProvider serviceProvider = new ServiceCollection()
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

            return serviceProvider;
        }
    }
}
