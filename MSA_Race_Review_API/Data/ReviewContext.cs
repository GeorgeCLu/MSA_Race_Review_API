using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MSA_Race_Review_API;
using System;

public class ReviewContext : DbContext
{
    // an empty constructor
    public ReviewContext() { }

    // base(options) calls the base class's constructor,
    // in this case, our base class is DbContext
    public ReviewContext(DbContextOptions<ReviewContext> options) : base(options) { }

    // Use DbSet<Student> to query or read and 
    // write information about A Student
    public DbSet<Race> Race { get; set; }
    public DbSet<Review> Review { get; set; }
    public static System.Collections.Specialized.NameValueCollection AppSettings { get; }

    // configure the database to be used by this context
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
       .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
       .AddJsonFile("appsettings.json")
	   .AddEnvironmentVariables()
       .Build();

        // schoolSIMSConnection is the name of the key that
        // contains the has the connection string as the value
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("reviewSIMSConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //  modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.reviewText).IsUnicode(false);

            entity.Property(e => e.reviewScore).IsUnicode(false);

            entity.Property(e => e.reviewerName).IsUnicode(false);

            entity.Property(e => e.upvotes).IsUnicode(false);

            entity.Property(e => e.timeCreated).IsUnicode(false);

            entity.HasOne<Race>()
                   .WithMany(p => p.Review)
                   .HasForeignKey(d => d.raceId);
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.Property(e => e.raceName).IsUnicode(false);

            entity.Property(e => e.championship).IsUnicode(false);

            entity.Property(e => e.year).IsUnicode(false);

            entity.Property(e => e.track).IsUnicode(false);

            entity.Property(e => e.location).IsUnicode(false);

            entity.Property(e => e.averageScore).IsUnicode(false);

            entity.Property(e => e.scoreSum).IsUnicode(false);

            entity.Property(e => e.totalReviews).IsUnicode(false);

        });
    }
}