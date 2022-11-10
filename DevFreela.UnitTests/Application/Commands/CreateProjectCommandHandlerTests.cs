using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        private readonly Fixture _fixture = new Fixture();
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnsProjectId()
        {
            //arrange
            var createProjectCommand = _fixture.Create<CreateProjectCommand>();
  
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepositoryMock.Object);
            
            //act
            var projectId = await createProjectCommandHandler.Handle(createProjectCommand, 
                new CancellationToken());

            //assert
            Assert.True(projectId >= 0);
            projectRepositoryMock.Verify(_ => _.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
