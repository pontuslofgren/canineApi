using Microsoft.EntityFrameworkCore;
using canineApi.Models;

    public class CanineContext : DbContext
    {
        public CanineContext (DbContextOptions<CanineContext> options)
            : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; } = default!;
    }