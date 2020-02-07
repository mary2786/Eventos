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
        public EventService(IEventRepository eventRepository, IDateEventUtil dateEventUtil, ICurrentDate currentDate, IDateConverter dateConverter)
        {
            _eventRepository = eventRepository;
            _dateConverter = dateConverter;
            _dateEventUtil = dateEventUtil;
            _currentDate = currentDate;
        }

        public string[] GetEvents()
        {
            return _eventRepository.GetEvents();
        }

        public string GetTextEvent(string @event, CultureInfo cultureInfo)
        {
            string[] eventInformation = @event.Split(",".ToCharArray());
            DateTime dateEvent = _dateConverter.ConverterTextToDate(eventInformation[1], cultureInfo);
            return _dateEventUtil.GetMessageEvent(eventInformation[0], _currentDate.GetCurrentDate(), dateEvent);
        }
    }
}
