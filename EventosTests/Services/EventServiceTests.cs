using Eventos.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Eventos.Services.Tests
{
    [TestClass()]
    public class EventServiceTests
    {
        private Mock<IEventRepository> _eventRepository;
        private Mock<IDateEventUtil> _dateEventUtil;
        private Mock<IPrintEvent> _printEvent;
        private Mock<IEventValidator> _eventValidator;
        private EventService _eventService;
        private string[] _events;
        private string _path;

        [TestInitialize]
        public void Setup()
        {
            _eventRepository = new Mock<IEventRepository>();
            _dateEventUtil = new Mock<IDateEventUtil>();
            _printEvent = new Mock<IPrintEvent>();
            _eventValidator = new Mock<IEventValidator>();
            _eventService = new EventService(_eventRepository.Object, _dateEventUtil.Object, _printEvent.Object, _eventValidator.Object);
            _events = new string[] { "Día de las madres, 10/05/2019 10:50:20", "Navidad, 25/12/2020 12:01:00" };
            _path = @"c:\Temp\eventos.txt";

            _eventRepository.Setup(s => s.GetEvents(It.IsAny<string>())).Returns(_events);
            _printEvent.Setup(s => s.PrintTextEvent(It.IsAny<string>()));
        }

        [TestMethod()]
        public void PrintEvents_CorrectEventData_PrintMessageEvent()
        {
            //Arrange    
            Event @event1 = new Event() { Name = "Día de las madres", Date = new DateTime(2019, 5, 10, 10, 50, 20) };
            Event @event2 = new Event() { Name = "Navidad", Date = new DateTime(2020, 12, 25, 12, 1, 0) };
            string messageEventExp1 = "Día de las madres ocurrió hace 9 meses";
            string messageEventExp2 = "Navidad ocurrirá dentro de 10 meses";
            
            _eventValidator.SetupSequence(s => s.ValidateEventFormat(It.IsAny<string>())).Returns(@event1).Returns(event2);
            _dateEventUtil.SetupSequence(s => s.GetMessageEvent(It.IsAny<Event>())).Returns(messageEventExp1).Returns(messageEventExp2);    

            //Act
            _eventService.PrintEvents(_path);

            //Assert
            _eventRepository.Verify(v => v.GetEvents(It.IsAny<string>()), Times.Once);
            _eventValidator.Verify(v => v.ValidateEventFormat(It.IsAny<string>()), Times.Exactly(2));
            _dateEventUtil.Verify(v => v.GetMessageEvent(It.IsAny<Event>()), Times.Exactly(2));
            _printEvent.Verify(v => v.PrintTextEvent(It.IsAny<string>()), Times.Exactly(2));
        }


        [TestMethod()]
        public void PrintEvents_IncorrectFormatEvent_PrintException()
        {
            //Arrange
            _eventValidator.Setup(s => s.ValidateEventFormat(It.IsAny<string>())).Throws(new Exception("Error"));

            //Act
            _eventService.PrintEvents(_path);

            //Assert
            _printEvent.Verify(v => v.PrintTextEvent(It.IsAny<string>()), Times.Once);
        }
    }
}