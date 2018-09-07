using WebAPI.Core.Domain;
using WebAPI.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
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
            builder.ApplyConfiguration(new CandidateConfiguration());
            builder.ApplyConfiguration(new RefreshUserTokenConfiguration());
            // custom Base configuration
            builder.ApplyConfiguration(new ApplicationConfiguration());
            builder.ApplyConfiguration(new PartyConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            base.OnModelCreating(builder);
        }
        #endregion

        #region Properties
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<RefreshUserToken> RefreshUserTokens { get; set; }
        #endregion
    }
}