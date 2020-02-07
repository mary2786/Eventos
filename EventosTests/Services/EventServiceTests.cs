using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eventos.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Eventos.Persistencia;
using System.Globalization;

namespace Eventos.Services.Tests
{
    [TestClass()]
    public class EventServiceTests
    {
        private Mock<IEventRepository> _eventRepository;
        private Mock<IDateConverter> _dateConverter;
        private Mock<IDateEventUtil> _dateEventUtil;
        private Mock<ICurrentDate> _currentDate;
        private Mock<IPrintEvent> _printEvent;
        private EventService _eventService;

        [TestInitialize]
        public void Setup()
        {
            _eventRepository = new Mock<IEventRepository>();
            _dateConverter = new Mock<IDateConverter>();
            _dateEventUtil = new Mock<IDateEventUtil>();
            _currentDate = new Mock<ICurrentDate>();
            _printEvent = new Mock<IPrintEvent>();
            _eventService = new EventService(_eventRepository.Object, _dateEventUtil.Object, _currentDate.Object, _dateConverter.Object, _printEvent.Object);
        }

        [TestMethod()]
        public void PrintEvents_EventArray()
        {
            //Arrange
            string path = @"c:\Temp\eventos.txt";
            string[] eventsExp = { "Día de las madres, 10/05/2019 10:50:20", "Navidad, 25/12/2020 12:01:00", "Festival de la escuela, 02/09/2020 10:50:20"};
            _eventRepository.Setup(s => s.GetEvents(It.IsAny<string>())).Returns(eventsExp);
            _printEvent.Setup(s => s.PrintTextEvent(It.IsAny<string>()));

            //Act
            _eventService.PrintEvents(path);

            //Assert
            _eventRepository.Verify(v => v.GetEvents(It.IsAny<string>()), Times.Once);
            _printEvent.Verify(v => v.PrintTextEvent(It.IsAny<string>()), Times.Exactly(3));
        }

        [TestMethod()]
        public void GetTextEvent_FullTextOfTheEvent()
        {
            // Arrange
            string @event = "Evento X, 10/05/2019 10:50:20";
            string textEventExp = "Evento X ocurrirá dentro de 20 minutos";
            _dateConverter.Setup(s => s.ConverterTextToDate(It.IsAny<string>())).Returns(It.IsAny<DateTime>());
            _dateEventUtil.Setup(s => s.GetMessageEvent(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(textEventExp);

            //Act
            string textEventAct = _eventService.GetTextEvent(@event);

            //Assert
            Assert.AreEqual(textEventExp, textEventAct);
            _dateConverter.Verify(v => v.ConverterTextToDate(It.IsAny<string>()), Times.Once);
            _dateEventUtil.Verify(v => v.GetMessageEvent(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}