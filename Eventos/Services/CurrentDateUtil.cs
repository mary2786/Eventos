using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.Services
{
    public class CurrentDateService : ICurrentDateService
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
