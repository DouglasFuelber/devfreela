using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class UpdateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ProjectPropertiesAreUpdated()
        {
            // Arrange
            var project = new Project("Test project", "Test project description", 1, 2, 500);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>()).Result).Returns(project);

            string newTitle = "Project Title Updated";
            string newDescription = "Project Description Updated";
            decimal newTotalCost = 1000;            

            var updateProjectCommand = new UpdateProjectCommand
            {
                Title = newTitle,
                Description = newDescription,
                TotalCost = newTotalCost
            };
            var updateProjectCommandHandler = new UpdateProjectCommandHandler(projectRepositoryMock.Object);

            //// Act
            await updateProjectCommandHandler.Handle(updateProjectCommand, new CancellationToken());

            //// Assert
            Assert.Equal(project.Title, newTitle);
            Assert.Equal(project.Description, newDescription);
            Assert.Equal(project.TotalCost, newTotalCost);

            projectRepositoryMock.Verify(p => p.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
