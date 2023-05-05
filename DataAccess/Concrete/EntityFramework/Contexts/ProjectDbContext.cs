using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Because this context is followed by migration for more than one provider
    /// works on PostGreSql db by default. If you want to pass sql
    /// When adding AddDbContext, use MsDbContext derived from it.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        /// <summary>
        /// in constructor we get IConfiguration, parallel to more than one db
        /// we can create migration.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        /// <summary>
        /// Let's also implement the general version.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;

        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<MobileLogin> MobileLogins { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<Birim> Birims { get; set; }
        public DbSet<BirimAgac> BirimAgacs { get; set; }
        public DbSet<BirimAgacKullaniciRol> BirimAgacKullaniciRols { get; set; }
        public DbSet<BirimYetkiIslevObje> BirimYetkiIslevObjes { get; set; }
        public DbSet<Islev> Islevs { get; set; }
        public DbSet<Kod_BirlikTip> Kod_BirlikTips { get; set; }
        public DbSet<Kod_RolSeviye> Kod_RolSeviyes { get; set; }
        public DbSet<Kod_RolTip> Kod_RolTips { get; set; }
        public DbSet<KullaniciMenuIslevEngel> KullaniciMenuIslevEngels { get; set; }
        public DbSet<KullaniciMenuIslevObje> KullaniciMenuIslevObjes { get; set; }
        public DbSet<KullaniciRol> KullaniciRols { get; set; }
        public DbSet<KullaniciYetkiIslevEngel> KullaniciYetkiIslevEngels { get; set; }
        public DbSet<KullaniciYetkiIslevObje> KullaniciYetkiIslevObjes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Obje> Objes { get; set; }
        public DbSet<Proje> Projes { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<RolMenuIslevObje> RolMenuIslevObjes { get; set; }
        public DbSet<RolYetkiIslevObje> RolYetkiIslevObjes { get; set; }
        public DbSet<Yetki> Yetkis { get; set; }

        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DArchPgContext"))
                    .EnableSensitiveDataLogging());
            }
        }
    }
}
