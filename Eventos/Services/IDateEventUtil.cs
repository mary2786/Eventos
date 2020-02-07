using System;
using System.Globalization;

namespace Eventos.Services
{
    public interface IDateEventUtil
    {
        string ConvertTimeToText(TimeSpan timeInterval);
        string GetMessageEvent(string nameEvent, DateTime dateEvent, DateTime dateNow);
    }
}