using System.Collections.Generic;

namespace Eventos.Persistencia
{
    public interface IEventRepository
    {
        Dictionary<string, string> GetEvents();
    }
}