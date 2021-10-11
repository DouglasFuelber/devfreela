using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task AddCommentAsync(ProjectComment projectComment)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public Task<Project> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> GetDetailsByIdAsync(int id)
        {
            return await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
