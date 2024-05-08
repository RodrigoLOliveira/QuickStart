using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Authorization;
using Template.Domain.Entities;

namespace Template.Infra.Contexts
{
    public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TemplateDbContext).Assembly);
        }


        #region DbSets


        #endregion

    }
}
