using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Eventos.Services.Tests
{
    [TestClass()]
    public class DateEventUtilTests
    {
        private Mock<ITimeInterval> _timeInterval;
        private Mock<ICurrentDate> _currentDate;
        private DateEventUtil _dateEventUtil;

        [TestInitialize]
        public void Setup()
        {
            _timeInterval = new Mock<ITimeInterval>();
            _currentDate = new Mock<ICurrentDate>();
            _dateEventUtil = new DateEventUtil(_timeInterval.Object, _currentDate.Object);
        }

        [TestMethod()]
        [DataRow(35, 23, 59, 59)]
        [DataRow(0, 720, 59, 59)]
        [DataRow(0, 0, 43200, 59)]
        [DataRow(0, 0, 0, 2592000)]
        public void ConvertTimeToText_TimespanForAMonth_TextIndicatingOneMonth(int days, int hours, int minutes, int seconds)
        {
            //Arrange
            string textExp = "1 mes";
            TimeSpan timeSpan = new TimeSpan(days, hours, minutes, seconds);

            //Act
            string textAct = _dateEventUtil.ConvertTimeToText(timeSpan);

            //Assert
            Assert.AreEqual(textExp, textAct);
        }

        [TestMethod()]
        public void ConvertTimeToText_TimespanForMoreThanOneMonth_TextIndicatingMonths()
        {
            //Arrange
            string textExp = "2 meses";
            TimeSpan timeSpan = new TimeSpan(60, 23, 59, 59);

            //Act
            string textAct = _dateEventUtil.ConvertTimeToText(timeSpan);

            //Assert
            Assert.AreEqual(textExp, textAct);
        }

        [TestMethod()]
        [DataRow(1, 10, 2, 3)]
        [DataRow(0, 24, 59, 59)]
        [DataRow(0, 0, 1440, 59)]
        [DataRow(0, 0, 0, 86400)]
        public void ConvertTimeToText_TimespanForADay_TextIndicatingOneDay(int days, int hours, int minutes, int seconds)
        {
            //Arrange
            string textExp = "1 día";
            TimeSpan timeSpan = new TimeSpan(days, hours, minutes, seconds);

            //Act
            string textAct = _dateEventUtil.ConvertTimeToText(timeSpan);

            //Assert
            Assert.AreEqual(textExp, textAct);
        }

        [TestMethod()]
        public void ConvertTimeToText_TimespanForMoreThanOneDay_TextIndicatingDays()
        {
            //Arrange
            string textExp = "15 días";
            TimeSpan timeSpan = new TimeSpan(15, 10, 2, 3);

            //Act
            string textAct = _dateEventUtil.ConvertTimeToText(timeSpan);

            //Assert
            Assert.AreEqual(textExp, textAct);
        }

        [TestMethod()]
        [DataRow(1, 59, 59)]
        [DataRow(0, 60, 59)]
        [DataRow(0, 0, 3600)]
        public void ConvertTimeToText_TimespanForATime_TextIndicatingOneHour(int hours, int minutes, int seconds)
        {
            //Arrange
            string textExp = "1 hora";
            TimeSpan timeSpan = new TimeSpan(hours, minutes, seconds);

            //Act
            string textAct = _dateEventUtil.ConvertTimeToText(timeSpan);

            //Assert
            Assert.AreEqual(textExp, textAct);
        }

        [TestMethod()]
        public void ConvertTimeToText_TimespanForMoreThanOneHours_TextIndicatingHours()
        {
            //Arrange
            string textExp = "2 horas";
            TimeSpan timeSpan = new TimeSpan(2, 59, 59);

            //Act
            string textAct = _dateEventUtil.ConvertTimeToText(timeSpan);

            //Assert
            Assert.AreEqual(textExp, textAct);
        }

        [TestMethod()]
        [DataRow(1, 59)]
        [DataRow(0, 60)]
        public void ConvertTimeToText_TimespanForAMinute_TextIndicatingOneMinute(int minutes, int seconds)
        {
            //Arrange
            string textExp = "1 minuto";
            TimeSpan timeSpan = new TimeSpan(0, minutes, seconds);

            //Act
            string textAct = _dateEventUtil.ConvertTimeToText(timeSpan);

            //Assert
            Assert.AreEqual(textExp, textAct);
        }

        [TestMethod()]
        public void ConvertTimeToText_TimespanForMoreThanOneMinute_TextIndicatingMinutes()
        {
            //Arrange
            string textExp = "10 minutos";
            TimeSpan timeSpan = new TimeSpan(0, 10, 59);

            //Act
            string textAct = _dateEventUtil.ConvertTimeToText(timeSpan);

            //Assert
            Assert.AreEqual(textExp, textAct);
        }

        
        [TestMethod()]
        public void GetMessageEvent_DateEventIsGreaterThanDateNow_MessageIndicatesThatTheEventWillOccurSoon()
        {
            //Arrange
            string messageEventExp = "San Valentín ocurrirá dentro de 7 días";
            DateTime dateNow = new DateTime(2020, 2, 7);
            TimeSpan timeInterval = new TimeSpan(7,0, 0, 0);
            Event @event = new Event() { Name = "San Valentín", Date = new DateTime(2020, 2, 14) };
            _currentDate.Setup(s => s.GetCurrentDate()).Returns(dateNow);
            _timeInterval.Setup(s => s.GetTimeInterval(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(timeInterval);

            //Act
            string messageEventAct = _dateEventUtil.GetMessageEvent(@event);

            //Assert
            _currentDate.Verify(v => v.GetCurrentDate(), Times.Once);
            _timeInterval.Verify(v => v.GetTimeInterval(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
            Assert.AreEqual(messageEventExp, messageEventAct);
        }

        [TestMethod()]
        public void GetMessageEvent_DateEventIsLessThanDateNow_MessageIndicatesThatTheEventHasAlreadyOccurred()
        {
            //Arrange
            string messageEventExp = "Evento X ocurrió hace 6 días";
            DateTime dateNow = new DateTime(2020, 2, 7);
            TimeSpan timeInterval = new TimeSpan(6, 0, 0, 0);
            _currentDate.Setup(s => s.GetCurrentDate()).Returns(dateNow);
            _timeInterval.Setup(s => s.GetTimeInterval(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(timeInterval);
            Event @event = new Event() { Name = "Evento X", Date = new DateTime(2020, 2, 1) };

            //Act
            string messageEventAct = _dateEventUtil.GetMessageEvent(@event);

            //Assert
            _currentDate.Verify(v => v.GetCurrentDate(), Times.Once);
            _timeInterval.Verify(v => v.GetTimeInterval(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
            Assert.AreEqual(messageEventExp, messageEventAct);
        }
    }
}