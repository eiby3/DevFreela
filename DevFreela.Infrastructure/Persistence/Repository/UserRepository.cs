using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public UserRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<User> GetByIdAsync(int id) 
            => await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
    }
}
