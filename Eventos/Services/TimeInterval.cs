using System;

namespace Eventos.Services
{
    public class TimeInterval : ITimeInterval
    {
        public TimeSpan GetTimeInterval(DateTime dateNow, DateTime dateEvent)
        {
            TimeSpan timeInterval = dateNow - dateEvent;

            if (timeInterval.TotalMilliseconds < 0)
            {
                timeInterval *= -1;
            }
            
            return timeInterval;
        }
    }
}
