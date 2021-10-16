using DevFreela.Application.Commands.CreateComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateCommentCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_CommentAddIsExecuted()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateCommentCommand
            {
                IdProject = 1,
                IdUser = 1,
                Content = "Comment content"
            };
            var createCommentCommandHandler = new CreateCommentCommandHandler(projectRepositoryMock.Object);

            // Act
            await createCommentCommandHandler.Handle(createProjectCommand, new CancellationToken());

            // Assert
            projectRepositoryMock.Verify(p => p.AddCommentAsync(It.IsAny<ProjectComment>()), Times.Once);
        }
    }
}
