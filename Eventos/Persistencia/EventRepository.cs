using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Eventos.Persistencia
{
    public class EventRepository : IEventRepository
    {

        public Dictionary<string, string> GetEvents()
        {
            string path = "C:\\Users\\elena.pech\\Documents\\Cursos BOT 2020\\Eventos\\Eventos\\Persistencia\\eventos.txt";
            string[] lines = File.ReadAllLines(path);
            Dictionary<string, string> events = new Dictionary<string, string>();

            string[] data;
            foreach (string line in lines)
            {
                data = line.Split(",".ToCharArray());
                if (!events.ContainsKey(data[0]))
                {
                    events.Add(data[0], data[1]);
                }
            }

            return events;
        }
    }
}
