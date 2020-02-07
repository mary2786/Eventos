using System.IO;

namespace Eventos.Persistencia
{
    public class EventRepository : IEventRepository
    {
        public string[] GetEvents()
        {
            string path = @"c:\Temp\eventos.txt";
            string[] lines = File.ReadAllLines(path);
            return lines;
        }
    }
}
