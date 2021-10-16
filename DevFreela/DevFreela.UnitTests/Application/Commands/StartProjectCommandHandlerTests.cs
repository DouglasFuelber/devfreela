using DevFreela.Application.Commands.StartProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class StartProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Executed_ProjectStatusIsInProgress()
        {
            // Arrange
            var project = new Project("Test project", "Description of test project", 1, 2, 1000);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>()).Result).Returns(project);

            var startProjectCommand = new StartProjectCommand(It.IsAny<int>());
            var startProjectCommandHandler = new StartProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await startProjectCommandHandler.Handle(startProjectCommand, new CancellationToken());

            // Assert
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
            projectRepositoryMock.Verify(p => p.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
