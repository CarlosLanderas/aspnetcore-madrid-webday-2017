using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PlainConcepts.WebDay.Infrastructure.DataContext;
using PlainConcepts.WebDay.Model;
using SQLitePCL;

namespace PlainConcepts.WebDay.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebDayDbContext _context;

        public UserRepository(WebDayDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user, CancellationToken token)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(token);
        }

        public async Task<int> RemoveAsync(int id, CancellationToken token)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new Exception("User not found");
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync(token);
        }
    }
}
