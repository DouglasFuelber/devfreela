using DevFreela.Core.Entities;
using System.Collections.Generic;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>();
            Users = new List<User>();
            Skills = new List<Skill>();
        }
        public List<Project> Projects{ get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
