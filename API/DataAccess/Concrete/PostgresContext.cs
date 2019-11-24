using Core.Helpers;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess.Concrete
{
    public class PostgresContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Area> Areas { get; set; }

        public readonly AppSettings appSettings;

        public PostgresContext()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();

            var appSettingsSection = configuration.GetSection("AppSettings");
            appSettings = appSettingsSection.Get<AppSettings>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(appSettings.ConnectionString);
        }
    }
}
