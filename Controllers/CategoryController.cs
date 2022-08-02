using Lab.Models;
using Lab.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        ICategoryRepository c;
        public CategoryController(ICategoryRepository cc)
        {
            c = cc;

        }
        [HttpGet]
        public IActionResult getAll()
        {
            List<Category> categories = c.GetAll();
            return Ok(categories);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Category cat)
        {
            int x = c.Update(id, cat);
            return Ok(x);

        }
      
        [HttpPost]
        public IActionResult New(Category cat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    c.Insert(cat);
                    string url = Url.Link("", new { id = cat.ID });
                    return Created(url, cat);// Created(url, dep);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            int x = c.Delete(id);
            return Ok(x);

        }
        [HttpGet("{Name:alpha}")]
        public IActionResult getByNAme(string Name)
        {
            if (c.getByNAme(Name) == null)
            {
                return BadRequest("There is no category with this name");
            }

            return Ok(c.getByNAme(Name));

        }
    }
}
