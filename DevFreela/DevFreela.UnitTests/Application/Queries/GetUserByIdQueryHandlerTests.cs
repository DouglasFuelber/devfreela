using DevFreela.Application.Queries.GetUserById;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetUserByIdQueryHandlerTests
    {
        [Fact]
        public async void UserExists_Executed_ReturnUserViewModel()
        {
            // Arrange
            var user = new User("User test", "test@mail.com", new DateTime(1990, 1, 1), "mypassword", "client");

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<int>()).Result).Returns(user);

            var getUserByIdQuery = new GetUserByIdQuery(It.IsAny<int>());
            var getUserByIdQueryHandler = new GetUserByIdQueryHandler(userRepositoryMock.Object);

            // Act
            var userViewModel = await getUserByIdQueryHandler.Handle(getUserByIdQuery, new CancellationToken());

            // Assert
            Assert.NotNull(userViewModel);
            Assert.IsType<UserViewModel>(userViewModel);

            userRepositoryMock.Verify(p => p.GetByIdAsync(It.IsAny<int>()).Result, Times.Once);
        }
    }
}
