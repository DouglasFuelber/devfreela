using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUserId()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var createUserCommand = new CreateUserCommand
            {
                Fullname = "User test",
                Email = "test@mail.com",
                BirthDate = new DateTime(1990,1,1),
                Password = "mypassword",
                Role = "client"
            };
            var createUserCommandHandler = new CreateUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object);

            // Act
            var userId = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

            // Assert
            Assert.True(userId >= 0);

            authServiceMock.Verify(a => a.ComputeSha256Hash(It.IsAny<string>()), Times.Once);
            userRepositoryMock.Verify(u => u.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
