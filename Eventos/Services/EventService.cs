using Eventos.Persistencia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Eventos.Services
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Dictionary<string, string> GetEvents()
        {
            Dictionary<string, string> events = _eventRepository.GetEvents();
            Dictionary<string, string> eventsMod = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> keyValue in events)
            {
                DateTime dateEvent = DateTime.Parse(keyValue.Value, new CultureInfo("es-MX"));
                TimeSpan timeDiff = DateTime.Now - dateEvent;
                string text = "ocurrió hace ";
                if (DateTime.Compare(DateTime.Now, dateEvent) <= 0)
                {
                    text = "ocurrirá dentro de ";
                    timeDiff *= -1;
                }

                int months = timeDiff.Days/30;
                int days = timeDiff.Hours/24;
                int hours = timeDiff.Minutes/60;
                if (months > 0)
                {     
                    text += months.ToString() + " mes" + ((months > 1)? "es": "");
                }
                else if (days > 0)
                {
                    text += days.ToString() + " día" + ((days > 1) ? "s" : "");
                }
                else if (hours > 0)
                {
                    text += hours.ToString() + " hora" + ((hours > 1) ? "s" : "");
                }
                else
                {
                    int minutes = timeDiff.Minutes;
                    text += minutes.ToString() +  " minuto" + ((minutes > 1) ? "s" : "");
                }

                eventsMod.Add(keyValue.Key, text);
            }

            return eventsMod;
        }
    }
}
