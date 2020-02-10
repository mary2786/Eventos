using System;

namespace Eventos.Services
{
    public interface IDateEventUtil
    {
        string ConvertTimeToText(TimeSpan timeInterval);
        string GetMessageEvent(Event @event);
    }
}