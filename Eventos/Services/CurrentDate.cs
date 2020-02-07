using System;

namespace Eventos.Services
{
    public class CurrentDate : ICurrentDate
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
