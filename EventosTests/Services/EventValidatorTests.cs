using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Eventos.Services.Tests
{
    [TestClass()]
    public class EventValidatorTests
    {
        private Mock<IDateConverter> _dateConverter;
        private EventValidator _eventValidator;

        [TestInitialize]
        public void Setup()
        {
            _dateConverter = new Mock<IDateConverter>();
            _eventValidator = new EventValidator(_dateConverter.Object);
        }
       
        [TestMethod()]
        public void ValidateEventFormat_CorrectStringFormat_ArrangementWithEventData()
        {
            //Arrange
            Event eventExp = new Event() { Name = "Evento X", Date = new DateTime(2019, 5, 10, 10, 50, 20) };
            string textEvent = string.Format("{0},{1}", eventExp.Name, eventExp.Date.ToString());
            _dateConverter.Setup(s => s.ConverterTextToDate(It.IsAny<string>())).Returns(eventExp.Date);

            //Act
            Event @event = _eventValidator.ValidateEventFormat(textEvent);

            //Assert
            Assert.IsNotNull(@event);
            Assert.AreEqual(eventExp.Name, @event.Name);
            Assert.AreEqual(eventExp.Date, @event.Date);
        }

        [TestMethod()]
        public void ValidateEventFormat_WrongStringFormat_ThrowException()
        {
            //Arrange
            string @event = "Evento X";
            string messageExceptionExp = string.Format("El texto con los datos del evento no tiene el formato correcto. ({0})", @event);

            //Act
            FormatException exception = Assert.ThrowsException<FormatException>(()=> _eventValidator.ValidateEventFormat(@event));

            //Assert
            Assert.AreEqual(messageExceptionExp, exception.Message);
        }
    }
}