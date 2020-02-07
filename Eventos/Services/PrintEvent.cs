using System;

namespace Eventos.Services
{
    public class PrintEvent : IPrintEvent
    {
        public void PrintTextEvent(string message)
        {
            Console.WriteLine("\t" + message);
        }
    }
}
