using System.Collections.Generic;

namespace Eventos.Services
{
    public interface IEventService
    {
        Dictionary<string, string> GetEvents();
    }
}