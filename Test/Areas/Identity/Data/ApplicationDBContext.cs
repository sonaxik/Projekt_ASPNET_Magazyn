using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Test.Models;

namespace Test.Areas.Identity.Data;

public class ApplicationDBContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }
    public DbSet<Asortyment> Asortyment { get; set; }
    public DbSet<Zamowienia> Zamowienia { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Zamowienia>()
        .HasOne(z => z.User)
        .WithMany()
        .HasForeignKey(z => z.UserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Zamowienia>()
            .HasOne(z => z.Produkt)
            .WithMany()
            .HasForeignKey(z => z.ProduktId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    private class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(255);
            builder.Property(x => x.LastName).HasMaxLength(255);
        }
    }
}