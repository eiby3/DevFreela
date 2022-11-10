using AutoFixture;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTests
    {
        private readonly Fixture _fixture = new Fixture();
        [Fact]
        public async Task ManyProjectsExists_Executed_ReturnsProjectViewModels()
        {
            //arrange
            var projectListMock = _fixture.CreateMany<Project>().ToList();

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(projectListMock));

            var getAllProjectsQuery = new GetAllProjectsQuery(It.IsAny<string>());
            
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);
            //act
            var projects = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //assert
            Assert.NotNull(projects);
            Assert.NotEmpty(projects);
            Assert.Equal(projectListMock.Count, projects.Count);
            projectRepositoryMock.Verify(_ => _.GetAllAsync().Result, Times.Once);
        }
    }
}
