using System;
using System.Globalization;

namespace Eventos.Services
{
    public interface IDateConverter
    {
        DateTime ConverterTextToDate(string dateEvent);
    }
}