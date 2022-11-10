namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTest
    {
        private readonly Fixture _fixture = new Fixture();
        [Fact]
        public void InputProject_Started_ReturnEnumInProgress_And_NotNullStartedAt()
        {
            //arrange
            var project =  _fixture.Create<Project>();

            //act
            project.Start();

            //assert
            Assert.Equal(EnumProjectStatus.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
        }
    }
}
