using JobPortal.Core.Domain;
using JobPortal.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        #endregion

        #region Override
        protected override void OnModelCreating(ModelBuilder builder) {
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new JobConfiguration());
            base.OnModelCreating(builder);
        }
        #endregion

        #region Properties
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        #endregion
    }
}