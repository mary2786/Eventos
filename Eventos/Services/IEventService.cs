using System.Globalization;

namespace Eventos.Services
{
    public interface IEventService
    {
        void PrintEvents(string path);
        string GetTextEvent(string @event);
    }
}