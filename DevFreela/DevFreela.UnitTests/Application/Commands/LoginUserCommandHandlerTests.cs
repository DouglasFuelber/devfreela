using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.ViewModels;
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
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_LoginViewModel()
        {
            // Arrange
            var user = new User("User test", "test@mail.com", new DateTime(1990, 1, 1), "mypassword", "client");

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.GetByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()).Result).Returns(user);

            var authServiceMock = new Mock<IAuthService>();

            var loginUserCommand = new LoginUserCommand
            {
                Email = user.Email,
                Password = user.Password
            };
            var loginUserCommandHandler = new LoginUserCommandHandler(authServiceMock.Object, userRepositoryMock.Object);

            // Act
            var loginUserViewModel = await loginUserCommandHandler.Handle(loginUserCommand, new CancellationToken());

            // Assert
            Assert.NotNull(loginUserViewModel);
            Assert.IsType<LoginUserViewModel>(loginUserViewModel);

            authServiceMock.Verify(a => a.ComputeSha256Hash(It.IsAny<string>()), Times.Once);
            authServiceMock.Verify(a => a.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            userRepositoryMock.Verify(u => u.GetByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
