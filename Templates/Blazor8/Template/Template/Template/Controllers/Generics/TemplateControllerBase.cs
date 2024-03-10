using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Template.Infra.Contexts;

namespace Template.Server.Controllers.Generics
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class TemplateControllerBase : ControllerBase
    {
        protected readonly TemplateDbContext TemplateDbContext;

        protected TemplateControllerBase(TemplateDbContext templateDbContext)
        {
            TemplateDbContext = templateDbContext;
        }
    }
}
