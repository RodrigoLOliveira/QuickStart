using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Business.Interfaces;
using Template.Domain.Entities;
using Template.Infra.Data;

namespace Template.Business.Services
{
    public abstract class CrudController<TEntity> : ControllerBase, ICrudController<TEntity> 
        where TEntity : EntityBase
    {
        public ApplicationDbContext DbContext { get; }

        public CrudController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IActionResult Create(TEntity model)
        {
            DbContext.Set<TEntity>().Add(model);
            return new OkObjectResult(model);
        }

        public IActionResult Delete(int id)
        {
            var model = DbContext.Set<TEntity>().Find(id);
            if (model == null)
                return new BadRequestObjectResult("Não encontrado!");

            DbContext.Set<TEntity>().Remove(model);
            return new OkResult();
        }

        public IActionResult Read(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult ReadAll()
        {
            throw new NotImplementedException();
        }

        public IActionResult Update(int id, TEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
