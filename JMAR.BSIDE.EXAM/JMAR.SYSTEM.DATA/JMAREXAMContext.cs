using JMAR.SYSTEM.DATA.Configuration;
using JMAR.SYSTEM.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace JMAR.SYSTEM.DATA
{
    public class JMAREXAMContext: DbContext
    {

        private string _dbName;

        public JMAREXAMContext(DbContextOptions<JMAREXAMContext> options) : base(options)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            this.Database.EnsureCreated();
        }

        public JMAREXAMContext(string dbName)
        {

            _dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(_dbName))
            {

                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                _dbName = configuration.GetConnectionString("Connection");

            }
            //base.OnConfiguring(optionsBuilder);
            if (!string.IsNullOrEmpty(_dbName))
                optionsBuilder.UseSqlServer(_dbName);
            else
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Initial Catalog=PPCTest;Persist Security Info=False;user id=sa;password=Andrea0187;Integrated Security=True;Connection Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users").HasKey(p => p.IdUser);
            modelBuilder.Entity<Users>().Property(p => p.IdUser).UseSqlServerIdentityColumn();

            modelBuilder.Entity<Products>().ToTable("Products").HasKey(p => p.IdProduct);
            modelBuilder.Entity<Products>().Property(p => p.IdProduct).UseSqlServerIdentityColumn();

            base.OnModelCreating(modelBuilder);

            new UsuariosConfiguration(modelBuilder.Entity<Users>());
            new ProductConfiguration(modelBuilder.Entity<Products>());

        }

    }
}
