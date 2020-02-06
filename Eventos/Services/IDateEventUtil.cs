using System;
using System.Collections.Generic;
using System.Globalization;

namespace Eventos.Services
{
    public interface IDateEventUtil
    {
        string ConvertTimeToText(DataEvent dataEvent);
        DataEvent GetDataEvent(DateTime dateNow, string[] eventInformation, CultureInfo cultureInfo);
    }
}