using System;

namespace Eventos.Services
{
    public interface ITimeInterval
    {
        TimeSpan GetTimeInterval(DateTime dateNow, DateTime dateEvent);
    }
}