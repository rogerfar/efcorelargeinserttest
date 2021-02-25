using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4
{
    public class DataContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=.;Initial Catalog=Test;Integrated Security=True;MultipleActiveResultSets=True");

    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Data { get; set; }
    }
}
