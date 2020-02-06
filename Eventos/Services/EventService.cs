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
        private IDateEventUtil _dateEventUtil;
        private ICurrentDateService _currentDateService;
        public EventService(IEventRepository eventRepository, IDateEventUtil dateEventUtil, ICurrentDateService currentDateService)
        {
            _eventRepository = eventRepository;
            _dateEventUtil = dateEventUtil;
            _currentDateService = currentDateService;
        }

        public string[] GetEvents()
        {
            return _eventRepository.GetEvents();
        }

        public string GetTextEvent(string @event, CultureInfo cultureInfo)
        {
            string[] eventInformation = @event.Split(",".ToCharArray());
            DataEvent dataEvent = _dateEventUtil.GetDataEvent(_currentDateService.GetCurrentDate(), eventInformation, cultureInfo);
            return _dateEventUtil.ConvertTimeToText(dataEvent);
        }
    }
}
