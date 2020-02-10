using System.IO;

namespace Eventos.Persistence
{
    public class EventRepository : IEventRepository
    {
        private readonly IFileWrapper _fileWrapper;
        public EventRepository(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }

        public string[] GetEvents(string path)
        {
            if (!_fileWrapper.Exists(path))
            {
                throw new DirectoryNotFoundException("No se encontró el archivo");
            }

            return _fileWrapper.ReadFile(path);
        }
    }
}
