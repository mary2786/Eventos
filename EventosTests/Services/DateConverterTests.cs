using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eventos.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Eventos.Services.Tests
{
    [TestClass()]
    public class DateConverterTests
    {
        [TestMethod()]
        public void ConverterTextToDate_CorrectlyFormattedString_DateTimeCorrectly()
        {
            //Arrange
            string stringTime = "07/02/2020 08:30:20";
            DateTime dateTimeExp = new DateTime(2020, 2, 7, 8, 30,20);

            //Act
            DateConverter dateConverter = new DateConverter();
            DateTime dateTimeAct = dateConverter.ConverterTextToDate(stringTime);

            //Assert
            Assert.AreEqual(dateTimeExp, dateTimeAct);
        }

        [TestMethod()]
        [DataRow("")]
        [DataRow("07-02-2020 08:30:20")]
        [DataRow("2020/07/02")]
        [DataRow("20/20/2020")]
        [DataRow("343243453455345")]
        [DataRow("2/2/2 08:00")]
        public void ConverterTextToDate_IncorrectlyFormattedString_ThrowFormatException(string dateFormatIncorrect)
        {
            //Arrange
            string errorExp = "No se pudo convertir el string '"+ dateFormatIncorrect + "' a fecha. El formato del string, debe ser 'dd/MM/yyyy hh:mm:ss'";

            //Act
            DateConverter dateConverter = new DateConverter();
            FormatException exception = Assert.ThrowsException<FormatException>(()=>dateConverter.ConverterTextToDate(dateFormatIncorrect));

            //Assert
            Assert.AreEqual(errorExp, exception.Message);
        }
    }
}
