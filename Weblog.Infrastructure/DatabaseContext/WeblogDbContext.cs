using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;

namespace Weblog.Infrastructure.DatabaseContext
{
    public class WeblogDbContext : IdentityDbContext<IdentityUser>
    {
        public WeblogDbContext(DbContextOptions<WeblogDbContext> options): base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Default values
            builder.Entity<Post>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");
            builder.Entity<Post>()
                .Property(p => p.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<Comment>()
                .Property(c => c.Id)
                .HasDefaultValueSql("NEWID()");
            builder.Entity<Comment>()
                .Property(c => c.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Entity<Comment>()
                .Property(c => c.IsApproved)
                .HasDefaultValue(false);
        }
    }

}
