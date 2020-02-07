using Eventos.Persistencia;
using System;
using System.Globalization;

namespace Eventos.Services
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;
        private IDateConverter _dateConverter;
        private IDateEventUtil _dateEventUtil;
        private ICurrentDate _currentDate;
        private IPrintEvent _printEvent;

        public EventService(IEventRepository eventRepository, 
            IDateEventUtil dateEventUtil, 
            ICurrentDate currentDate, 
            IDateConverter dateConverter, IPrintEvent printEvent)
        {
            _eventRepository = eventRepository;
            _dateConverter = dateConverter;
            _dateEventUtil = dateEventUtil;
            _currentDate = currentDate;
            _printEvent = printEvent;
        }

        public void PrintEvents(string path)
        {
            try
            {
                foreach (string @event in _eventRepository.GetEvents(path))
                {
                    string textEvent = GetTextEvent(@event);
                    _printEvent.PrintTextEvent(textEvent);
                }
            }
            catch (Exception excepcion)
            {
                throw new Exception("No se pudo imprimir la lista de eventos (" + excepcion + ")");
            }
        }

        public string GetTextEvent(string @event)
        {
            string[] eventInformation = @event.Split(",".ToCharArray());
            DateTime dateEvent = _dateConverter.ConverterTextToDate(eventInformation[1]);
            return _dateEventUtil.GetMessageEvent(eventInformation[0], _currentDate.GetCurrentDate(), dateEvent);
        }
    }
}
