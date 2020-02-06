using System.Collections.Generic;

namespace Eventos.Persistencia
{
    public interface IEventRepository
    {
        string[] GetEvents();
    }
}