using Template.Business.Repositories.Generics;
using Template.Domain.Entities;
using Template.Infra.Contexts;

namespace Template.Business.Repositories
{
    public class HomeRepository : RepositoryBase<Home>
    {
        public HomeRepository(TemplateDbContext context) : base(context)
        {

        }
    }
}
