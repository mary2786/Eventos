using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Eventos.Services.Tests
{
    [TestClass()]
    public class TimeIntervalTests
    {
        [TestMethod()]
        public void GetTimeInterval_DateEventIsGreaterThanDateNow_TotalDays()
        {
            //Arrange         
            DateTime dateEvent = new DateTime(2020, 2, 14);
            DateTime dateNow = new DateTime(2020, 2, 7);

            //Act
            TimeInterval timeInterval = new TimeInterval();
            TimeSpan interval = timeInterval.GetTimeInterval(dateNow, dateEvent);

            //Assert
            Assert.AreEqual(7, interval.Days);
        }

        [TestMethod()]
        public void GetTimeInterval_DateEventIsLessThanDateNow_TotalDays()
        {
            //Arrange         
            DateTime dateEvent = new DateTime(2020, 2, 1);
            DateTime dateNow = new DateTime(2020, 2, 7);

            //Act
            TimeInterval timeInterval = new TimeInterval();
            TimeSpan interval = timeInterval.GetTimeInterval(dateNow, dateEvent);

            //Assert
            Assert.AreEqual(6, interval.Days);
        }
    }
}