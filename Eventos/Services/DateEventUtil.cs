using System;

namespace Eventos.Services
{
    public class DateEventUtil : IDateEventUtil
    {
        private ITimeInterval _timeInterval;
        public DateEventUtil(ITimeInterval timeInterval)
        {
            _timeInterval = timeInterval;
        }

        public string ConvertTimeToText(TimeSpan timeInterval)
        {
            int months = (int)timeInterval.TotalDays / 30;
            int days = (int)timeInterval.TotalHours / 24;
            int hours = (int)timeInterval.TotalMinutes / 60;
            string text;

            if (months > 0)
            {
                text = months.ToString() + " mes" + ((months > 1) ? "es" : "");
            }
            else if (days > 0)
            {
                text = days.ToString() + " día" + ((days > 1) ? "s" : "");
            }
            else if (hours > 0)
            {
                text = hours.ToString() + " hora" + ((hours > 1) ? "s" : "");
            }
            else
            {
                int minutes = timeInterval.Minutes;
                text = minutes.ToString() + " minuto" + ((minutes > 1) ? "s" : "");
            }

            return text;
        }

        public string GetMessageEvent(string nameEvent, DateTime dateNow, DateTime dateEvent)
        {
            TimeSpan timeInterval = _timeInterval.GetTimeInterval(dateNow, dateEvent);
            string timeText = ConvertTimeToText(timeInterval);
            string message;
            if (DateTime.Compare(dateNow, dateEvent) <= 0)
            {
                message = "ocurrirá dentro de";
            }
            else
            {
                message = "ocurrió hace";
            }

            return string.Format("{0} {1} {2}", nameEvent, message,  timeText);
        }

        
    }
}
