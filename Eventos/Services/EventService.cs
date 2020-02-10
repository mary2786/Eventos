using Eventos.Persistence;
using System;

namespace Eventos.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;      
        private readonly IDateEventUtil _dateEventUtil;
        private readonly IPrintEvent _printEvent;
        private readonly IEventValidator _eventValidator;

        public EventService(IEventRepository eventRepository, 
            IDateEventUtil dateEventUtil, 
            IPrintEvent printEvent,
            IEventValidator eventValidator)
        {
            _eventRepository = eventRepository;
            _dateEventUtil = dateEventUtil;
            _printEvent = printEvent;
            _eventValidator = eventValidator;
        }

        public void PrintEvents(string path)
        {
            try
            {
                foreach (string textEvent in _eventRepository.GetEvents(path))
                {
                    string message = GetMessageEvent(textEvent);
                    _printEvent.PrintTextEvent(message);
                }
            }
            catch (Exception excepcion)
            {
                throw excepcion;
            }
        }

        private string GetMessageEvent(string textEvent)
        {
            Event @event = _eventValidator.ValidateEventFormat(textEvent);
            return _dateEventUtil.GetMessageEvent(@event);
        }
    }
}
