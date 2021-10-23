using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Payments;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class FinishProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectIsInProgress_Executed_ProjectStatusIsFinished()
        {
            // Arrange
            var project = new Project("Test project", "Description of test project", 1, 2, 1000);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>()).Result).Returns(project);

            var paymentService = new PaymentService();

            var finishProjectCommand = new FinishProjectCommand();
            var finsishProjectCommandHandler = new FinishProjectCommandHandler(projectRepositoryMock.Object, paymentService);

            // Act
            project.Start();
            await finsishProjectCommandHandler.Handle(finishProjectCommand, new CancellationToken());

            // Assert
            Assert.Equal(ProjectStatusEnum.Finished, project.Status);
            Assert.NotNull(project.FinishedAt);
            projectRepositoryMock.Verify(p => p.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
