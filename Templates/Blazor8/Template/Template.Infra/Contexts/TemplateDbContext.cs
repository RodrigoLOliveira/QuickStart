using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Authorization;
using Template.Domain.Entities;

namespace Template.Infra.Contexts
{
    public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Home> Homes { get; set; }
    }
}
