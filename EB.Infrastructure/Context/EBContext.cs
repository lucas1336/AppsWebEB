using EB.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EB.Infrastructure.Context;

public class EBContext : DbContext
{
    public EBContext()
    {
        
    }

    public EBContext(DbContextOptions<EBContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Snapshot> Snapshots { get; set; }
    
    protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=localhost,3306;Uid=root;Pwd=1234;Database=db_appsweb;", serverVersion);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>().ToTable("products");
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.SerialNumber).IsRequired();
        builder.Entity<Product>().Property(p => p.Brand).IsRequired();
        builder.Entity<Product>().Property(p => p.Model).IsRequired();
        builder.Entity<Product>().Property(p => p.MonitoringLevel).IsRequired();
        // builder.Entity<Product>().HasMany(p => p.Snapshots).WithOne(s => s.Product).HasForeignKey(s => s.ProductId);
        
        builder.Entity<Snapshot>().ToTable("snapshots");
        builder.Entity<Snapshot>().HasKey(s => s.Id);
        builder.Entity<Snapshot>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Snapshot>().Property(s => s.SnapshotId).IsRequired();
        builder.Entity<Snapshot>().Property(s => s.Temperature).IsRequired();
        builder.Entity<Snapshot>().Property(s => s.Energy);
        builder.Entity<Snapshot>().Property(s => s.Leakage);
        // builder.Entity<Snapshot>().HasOne(s => s.Product).WithMany(p => p.Snapshots).HasForeignKey(s => s.ProductId);
    }
}