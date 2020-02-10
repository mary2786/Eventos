using System;

namespace Eventos.Services
{
    public class EventValidator : IEventValidator
    {
        private readonly IDateConverter _dateConverter;

        public EventValidator(IDateConverter dateConverter)
        {
            _dateConverter = dateConverter;
        }

        public Event ValidateEventFormat(string textEvent)
        {
            string[] dataEvent = textEvent.Split(",".ToCharArray());
            if (dataEvent.Length < 2)
            {
                throw new FormatException("El texto con los datos del evento no tiene el formato correcto. (" + textEvent + ")");
            }

            DateTime date = _dateConverter.ConverterTextToDate(dataEvent[1]);

            Event @event = new Event()
            {
                Name = dataEvent[0],
                Date = date
            };

            return @event;
        }
    }
}
