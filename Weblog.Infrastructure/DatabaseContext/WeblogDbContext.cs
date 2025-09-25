using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Core.Domain.Entities;

namespace Weblog.Infrastructure.DatabaseContext
{
    public class WeblogDbContext : DbContext
    {
        public WeblogDbContext(DbContextOptions<WeblogDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
