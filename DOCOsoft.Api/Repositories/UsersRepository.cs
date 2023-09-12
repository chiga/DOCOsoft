using DOCOsoft.Api.Data;
using DOCOsoft.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOCOsoft.Api.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DOCOsoftContext _context;

    public UsersRepository(DOCOsoftContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User updatedUser)
    {
        _context.Users.Update(updatedUser);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Users.Where(user => user.Id == id).ExecuteDeleteAsync();
    }


}
