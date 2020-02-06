using System.Globalization;

namespace Eventos.Services
{
    public interface IEventService
    {
        string[] GetEvents();
        string GetTextEvent(string @event, CultureInfo cultureInfo);
    }
}