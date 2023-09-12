using DOCOsoft.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOCOsoft.Api.Data;

public class DOCOsoftContext : DbContext
{
    public DOCOsoftContext(DbContextOptions<DOCOsoftContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}