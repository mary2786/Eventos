using System;
using System.Globalization;

namespace Eventos.Services
{
    public class DateConverter : IDateConverter
    {
        public DateTime ConverterTextToDate(string dateEvent, CultureInfo cultureInfo)
        {
            return DateTime.Parse(dateEvent, cultureInfo);
        }
    }
}
