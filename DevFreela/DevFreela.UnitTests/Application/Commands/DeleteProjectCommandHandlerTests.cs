using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class DeleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Executed_ProjectStatusIsCancelled()
        {
            // Arrange
            var project = new Project("Test project", "Description of test project", 1, 2, 1000);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>()).Result).Returns(project);

            var deleteProjectCommand = new DeleteProjectCommand(It.IsAny<int>());
            var deleteProjectCommandHandler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await deleteProjectCommandHandler.Handle(deleteProjectCommand, new CancellationToken());

            // Assert
            Assert.Equal(ProjectStatusEnum.Canceled, project.Status);
            projectRepositoryMock.Verify(p => p.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
