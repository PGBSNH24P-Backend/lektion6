using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

    public DbSet<TodoEntity> Todos { get; set; }
    public DbSet<TagEntity> Tags { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostEntity>()
            .HasKey()
            .HasMany<CommentEntity>()
    }*/
}