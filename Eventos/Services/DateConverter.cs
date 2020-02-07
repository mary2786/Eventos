using System;
using System.Globalization;

namespace Eventos.Services
{
    public class DateConverter : IDateConverter
    {
        public DateTime ConverterTextToDate(string dateEvent)
        {
            if(!DateTime.TryParseExact(dateEvent, "dd/MM/yyyy hh:mm:ss", null, DateTimeStyles.None, out DateTime date)){
                throw new FormatException("No se pudo convertir el string '"+ dateEvent +"' a fecha. El formato del string, debe ser 'dd/MM/yyyy hh:mm:ss'");
            }

            return date;
        }
    }
}
