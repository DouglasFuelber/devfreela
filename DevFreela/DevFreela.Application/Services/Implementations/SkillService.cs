using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SkillViewModel> GetAll()
        {
            List<Skill> skills = _dbContext.Skills.ToList();

            var skillsViewModel = skills.Select(s => new SkillViewModel(s.Id, s.Description))
                                        .ToList();

            return skillsViewModel;
        }
    }
}
