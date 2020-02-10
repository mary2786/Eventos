using System;

namespace Eventos.Services
{
    public class DateEventUtil : IDateEventUtil
    {
        private readonly ITimeInterval _timeInterval;
        private readonly ICurrentDate _currentDate;

        public DateEventUtil(ITimeInterval timeInterval, ICurrentDate currentDate)
        {
            _timeInterval = timeInterval;
            _currentDate = currentDate;
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

        public string GetMessageEvent(Event @event)
        {
            DateTime dateNow = _currentDate.GetCurrentDate();
            TimeSpan timeInterval = _timeInterval.GetTimeInterval(dateNow, @event.Date);
            string timeText = ConvertTimeToText(timeInterval);
            string message;
            if (DateTime.Compare(dateNow, @event.Date) <= 0)
            {
                message = "ocurrirá dentro de";
            }
            else
            {
                message = "ocurrió hace";
            }

            return string.Format("{0} {1} {2}", @event.Name, message,  timeText);
        }

        
    }
}
