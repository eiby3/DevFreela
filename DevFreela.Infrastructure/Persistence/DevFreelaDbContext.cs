using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu projeto ASPNET Core 1", "Minha descricao do projeto", 1, 1, 10000),
                new Project("Meu projeto ASPNET Core 2", "Minha descricao do projeto", 1, 1, 20000),
                new Project("Meu projeto ASPNET Core 3", "Minha descricao do projeto", 1, 1, 30000)
            };

            Users = new List<User>
            {
                new User("Guilherme Abe", "abe@dev.com", new DateTime(2003, 04, 02)),
                new User("Alana Angelini", "alana@dev.com", new DateTime(2004, 04, 03)),
                new User("Azir Abe", "azir@dev.com", new DateTime(2020, 01, 18)),
            };

            Skills = new List<Skill>
            {
                new Skill(".NET Core"),
                new Skill("C#"),
                new Skill("SQL")
            };
        }
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }

        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }
    }
}
