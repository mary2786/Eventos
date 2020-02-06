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
                TimeSpan time = DateTime.Now - dateEvent;
                string text = "ocurrió hace ";
                if(DateTime.Compare(DateTime.Now, dateEvent)<=0)
                {
                    text = "ocurrirá dentro de ";
                }

                /*var d = dateEvent.Days;
                var h = dateEvent.Hours;
                var m = dateEvent.Minutes;*/

                eventsMod.Add(keyValue.Key, text);
            }

            return eventsMod;
        }

        /*
        public void getDate(DateTime dateEvent)
        {
            TimeSpan time = DateTime.Now - dateEvent;

        }*/
    }
}
