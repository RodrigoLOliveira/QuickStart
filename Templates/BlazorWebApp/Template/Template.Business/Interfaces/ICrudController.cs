using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Business.Interfaces
{
    public interface ICrudController<T>
    {
        public IActionResult Create(T model);
        public IActionResult ReadAll();
        public IActionResult Read(int id);
        public IActionResult Update(int id, T model);
        public IActionResult Delete(int id);

    }
}
