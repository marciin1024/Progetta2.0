using Progetta.Entities;
using Microsoft.EntityFrameworkCore;

namespace Progetta.Services
{
    public class UserService
    {
        private readonly ProjectContext _context;

        public UserService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users
                .OrderBy(u => u.FirstName)
                .ToListAsync();
        }
    }
}
