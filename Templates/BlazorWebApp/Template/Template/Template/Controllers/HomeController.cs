using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Template.Business;
using Template.Business.Services;
using Template.Domain.DTOs;
using Template.Domain.Entities;
using Template.Infra.Data;

namespace Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : CrudController<HomeDTO, HomeEntity>
    {
        public HomeController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
