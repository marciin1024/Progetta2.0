using Progetta.Entities;
using Microsoft.EntityFrameworkCore;

namespace Progetta.Services
{
    public class UserService
    {
        private readonly IDbContextFactory<ProjectContext> _contextFactory;

        public UserService(IDbContextFactory<ProjectContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            using ProjectContext context = _contextFactory.CreateDbContext();
            return await context.Users
                .OrderBy(u => u.FirstName)
                .ToListAsync();
        }
    }
}
