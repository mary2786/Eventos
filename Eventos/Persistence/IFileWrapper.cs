namespace Eventos.Persistence
{
    public interface IFileWrapper
    {
        bool Exists(string path);

        string[] ReadFile(string path);
    }
}
