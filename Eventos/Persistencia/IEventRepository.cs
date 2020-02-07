namespace Eventos.Persistencia
{
    public interface IEventRepository
    {
        string[] GetEvents(string path);
    }
}