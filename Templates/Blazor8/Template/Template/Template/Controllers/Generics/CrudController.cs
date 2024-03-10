using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Generics;
using Template.Infra.Contexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Template.Server.Controllers.Generics
{
    public abstract class CrudController<TEntity> : TemplateControllerBase
        where TEntity : EntityBase
    {
        protected CrudController(TemplateDbContext templateDbContext) 
            : base(templateDbContext)
        {
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            return await TemplateDbContext.Set<TEntity>().ToListAsync();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById(Guid id)
        {
            var entity = await TemplateDbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Create(TEntity entity)
        {
            TemplateDbContext.Set<TEntity>().Add(entity);
            await TemplateDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            TemplateDbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await TemplateDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!await EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var entity = await TemplateDbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            TemplateDbContext.Set<TEntity>().Remove(entity);
            await TemplateDbContext.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> EntityExists(Guid id)
        {
            return await TemplateDbContext.Set<TEntity>().AnyAsync(e => e.Id == id);
        }
    }
}
