using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public SkillRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<SkillDTO>> GetAllAsync()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "SELECT id, Description FROM Skills";

                var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

                return skills.ToList();

                //EF Core
                //var skills = _dbContext.Skills;

                //var skillsViewModel = skills
                //    .Select(_ => new SkillViewModel(_.Id, _.Description))
                //    .ToList();

                //return skillsViewModel;
            }
        }
    }
}
