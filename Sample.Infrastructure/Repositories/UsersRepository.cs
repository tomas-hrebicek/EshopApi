using Microsoft.EntityFrameworkCore;
using Sample.Domain.Domain;
using Sample.Domain.Entities;
using Sample.Infrastructure.Data;

namespace Sample.Infrastructure.Repositories
{
    internal class UsersRepository : Domain.Interfaces.IUsersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetAsync(string username)
        {
            return await _dbContext.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> InsertAsync(User user)
        {
            var insertedUser = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return insertedUser.Entity;
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedList<User>> ListAsync(PaginationSettings paginationSettings)
        {
            if (paginationSettings is null)
            {
                throw new ArgumentNullException(nameof(paginationSettings));
            }

            var query = _dbContext.Users.AsQueryable();
            return await query.ToPagedListAsync(paginationSettings);
        }
    }
}
