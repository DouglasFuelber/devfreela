using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetProjectByIdCommandHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Executed_ReturnProjectDetailsViewModel()
        {
            // Arrange
            var project = new Project("Test project", "Description of test project", 1, 2, 1000);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(p => p.GetDetailsByIdAsync(It.IsAny<int>()).Result).Returns(project);

            var getProjectByIdQuery = new GetProjectByIdQuery(It.IsAny<int>());
            var getProjectByIdQueryHandler = new GetProjectByIdQueryHandler(projectRepositoryMock.Object);

            // Act
            var projectViewModel = await getProjectByIdQueryHandler.Handle(getProjectByIdQuery, new CancellationToken());

            // Assert
            Assert.NotNull(projectViewModel);
            Assert.IsType<ProjectDetailsViewModel>(projectViewModel);

            projectRepositoryMock.Verify(p => p.GetDetailsByIdAsync(It.IsAny<int>()).Result, Times.Once);
        }
    }
}
