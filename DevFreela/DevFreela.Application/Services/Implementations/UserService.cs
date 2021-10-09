using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;
        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(CreateUserInputModel inputModel)
        {
            User user = new User(
                inputModel.Fullname, 
                inputModel.Email,
                inputModel.BirthDate);

            _dbContext.Users.Add(user);

            _dbContext.SaveChanges();

            return user.Id;
        }

        public UserViewModel GetById(int id)
        {
            User user = _dbContext.Users.SingleOrDefault(p => p.Id == id);

            if (user == null)
                return null;

            UserViewModel userViewModel = new UserViewModel(
                user.FullName,
                user.Email,
                user.BirthDate);

            return userViewModel;
        }
    }
}
