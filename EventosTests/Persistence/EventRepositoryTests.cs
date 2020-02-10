using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Linq;

namespace Eventos.Persistence.Tests
{
    [TestClass()]
    public class EventRepositoryTests
    {
        private Mock<IFileWrapper> _fileWrapper;
        private EventRepository _eventRepository;

        [TestInitialize]
        public void Setup()
        {
            _fileWrapper = new Mock<IFileWrapper>();
            _eventRepository = new EventRepository(_fileWrapper.Object);
        }

        [TestMethod()]
        public void GetEvents_FileNotExist_ThrowException()
        {
            //Arrange
            string path = "NotExist";
            string ExceptionMessageExp = "No se encontró el archivo";           
            _fileWrapper.Setup(s => s.Exists(It.IsAny<string>())).Returns(false);

            //Act           
            DirectoryNotFoundException exception = Assert.ThrowsException<DirectoryNotFoundException>(() => _eventRepository.GetEvents(path));

            //Assert
            Assert.AreEqual(ExceptionMessageExp, exception.Message);
        }

        [TestMethod()]
        public void GetEvents_FileExist_ArrayLine()
        {
            //Arrange
            string path = @"c:\Temp\eventos.txt";
            string[] eventsExp = { "Día de las madres, 10/05/2019 10:50:20", "Navidad, 25/12/2020 12:01:00", "Festival de la escuela, 02/09/2020 10:50:20" };
            _fileWrapper.Setup(s => s.Exists(It.IsAny<string>())).Returns(true);
            _fileWrapper.Setup(s => s.ReadFile(It.IsAny<string>())).Returns(eventsExp);

            //Act           
            string[] eventsAct = _eventRepository.GetEvents(path);

            //Assert
            CollectionAssert.AreEqual(eventsExp.ToList(), eventsAct.ToList());
            _fileWrapper.Verify(v => v.ReadFile(It.IsAny<string>()), Times.Once);
        }
    }
}