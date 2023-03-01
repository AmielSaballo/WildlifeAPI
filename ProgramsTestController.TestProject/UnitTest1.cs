using Moq;
using WildlifeAPI.Controllers;
using WildlifeAPI.Models;
using WildlifeAPI_Prod.Controllers;
using WildlifeAPI_Prod.Data.Services;

namespace ProgramsTestController.TestProject
{
    
    public class UnitTest1
    {
        private Mock<IProgramsService> service;

        public UnitTest1()
        {
            service = new Mock<IProgramsService>();
        }

        [Fact]
        public void GetProgramsById_Success_ReturnsNotNull()
        {
            var programList = new Programs() { id= 1, programName="Test", programSummary="This is a", programDescription="This is a test", phoneNumber="1111111" };
            service.Setup(x => x.GetById(1)).Returns(Task.Run(() => programList));
            var programsController = new TestProgramsController(service.Object);

            var result = programsController.GetProgramsById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllPrograms_Success_ReturnsNotNull()
        {
            var programList = CreateProgramList();
            service.Setup(x => x.GetAll()).Returns(Task.Run(() => programList));
            var programsController = new TestProgramsController(service.Object);

            var result = programsController.GetAllPrograms();

            Assert.NotNull(result);
        }

        public IEnumerable<Programs> CreateProgramList()
        {
            IEnumerable<Programs> programData = new List<Programs>();
            {
                new Programs() { id = 1, programName = "Test", programSummary = "This is a", programDescription = "This is a test", phoneNumber = "1111111", };
            }
            return programData;
        }
    }
}