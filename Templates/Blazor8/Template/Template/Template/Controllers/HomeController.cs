using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Infra.Contexts;
using Template.Server.Controllers.Generics;

namespace Template.Server.Controllers
{
    public class HomeController(TemplateDbContext templateDbContext) 
        : CrudController<Home>(templateDbContext)
    {
        [HttpGet("teste")]
        public async Task<IActionResult> Get()
        {
            throw new Exception("Teste");
            return Ok();
        }
    }
}
