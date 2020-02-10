namespace Eventos.Services
{
    public interface IEventValidator
    {
        Event ValidateEventFormat(string textEvent);
    }
}