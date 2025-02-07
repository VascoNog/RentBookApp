
namespace RentBookApp.Data;

public class RentBookDbContext : IdentityDbContext
{
    public RentBookDbContext(DbContextOptions<RentBookDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Rentals> Rentals { get; set; }
    public DbSet<Book> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Book Configurations
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasColumnType("nvarchar(50)");
            entity.Property(e => e.Category).HasColumnType("nvarchar(50)");
            entity.Property(e => e.Publisher).IsRequired().HasColumnType("nvarchar(50)");
            entity.Property(e => e.PublishedAt).IsRequired().HasColumnType("date"); // Só interessa a data
            entity.Property(e => e.ISBN).IsRequired().HasColumnType("nvarchar(15)");
            entity.HasIndex(e => e.ISBN).IsUnique();
        });

        //Rentals Configurations
        modelBuilder.Entity<Rentals>(entity =>
        {
            entity.Property(e => e.RentedAt).IsRequired().HasColumnType("datetime2(0)");
        });

        // Authors Configurations
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.AuthorName).IsRequired().HasColumnType("nvarchar(50)");
            entity.Property(e => e.BornAt).IsRequired().HasColumnType("date");
            entity.Property(e => e.Nationality).HasColumnType("nvarchar(20)");
        });
    }
}
