using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class PostgresContext : DbContext
    {
        public DbSet<Item> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=159.89.108.208;Port=5432;Database=postgres;User Id=kong;Password=esas10burda");
        }
    }
}
