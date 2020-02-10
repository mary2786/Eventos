namespace Eventos.Persistence
{
    public interface IEventRepository
    {
        string[] GetEvents(string path);
    }
}