using System;
using System.Collections.Generic;
using System.Globalization;

namespace Eventos.Services
{
    public class DateEventUtil : IDateEventUtil
    {
        public string ConvertTimeToText(DataEvent dataEvent)
        {
            int months = dataEvent.Time.Days / 30;
            int days = dataEvent.Time.Hours / 24;
            int hours = dataEvent.Time.Minutes / 60;
            string text = dataEvent.TextEvent;

            if (months > 0)
            {
                text += months.ToString() + " mes" + ((months > 1) ? "es" : "");
            }
            else if (days > 0)
            {
                text += days.ToString() + " día" + ((days > 1) ? "s" : "");
            }
            else if (hours > 0)
            {
                text += hours.ToString() + " hora" + ((hours > 1) ? "s" : "");
            }
            else
            {
                int minutes = dataEvent.Time.Minutes;
                text += minutes.ToString() + " minuto" + ((minutes > 1) ? "s" : "");
            }

            return text;
        }

        public DataEvent GetDataEvent(DateTime dateNow, string[] eventInformation, CultureInfo cultureInfo)
        {
            DataEvent dataEvent = new DataEvent();
            DateTime dateEvent = DateTime.Parse(eventInformation[1], cultureInfo);
            TimeSpan timeDiff = dateNow - dateEvent;
            dataEvent.TextEvent = eventInformation[0];
            if (DateTime.Compare(dateNow, dateEvent) <= 0)
            {
                dataEvent.TextEvent += " ocurrirá dentro de ";
                dataEvent.Time = timeDiff * -1;
            }
            else
            {
                dataEvent.TextEvent += " ocurrió hace ";
                dataEvent.Time = timeDiff;
            }

            return dataEvent;
        }
    }
}
