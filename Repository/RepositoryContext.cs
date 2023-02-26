using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions options) 
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ToDoTaskConfiguration());
        builder.ApplyConfiguration(new AccountConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<ToDoTask>? ToDoTasks { get; set; }
    public DbSet<Account>? Accounts { get; set; }
}
