using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PlainConcepts.WebDay.Model;

namespace PlainConcepts.WebDay.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken token);
        Task<int> RemoveAsync(int id, CancellationToken token);
    }
}
