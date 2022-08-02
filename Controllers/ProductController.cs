using Lab.Models;
using Lab.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProdutRepository p;
      
        public ProductController(IProdutRepository pp)
        {
            p = pp;

        }
        [HttpGet]
        public IActionResult getAll()
        {
            p.GetAll();
            return Ok();
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Product P)
        {
            p.Update(id, P);
            return Ok();

        }
   
        [HttpPost]
        public IActionResult New(Product P)
        {
            try
            {

                P.image=P.image.Split('\\')[2];
                        p.Insert(P);
                        string url = Url.Link("getOneRoute", new { id = P.ID });
                        return Ok("addedd");// Created(url, dep);
                    
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

        }
        [HttpPost("/image"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
               
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("id:int")]
        public IActionResult Delete(int id)
        {
            p.Delete(id);
            return Ok();

        }
        [HttpGet("{Name:alpha}")]
        public IActionResult getByNAme(string Name)
        {
            if (p.getByNAme(Name) == null)
            {
                return BadRequest("There is no product with this name");
            }
            return Ok(p.getByNAme(Name));

        }

        [HttpGet("{CatName:alpha}")]
        public IActionResult getBycatName(string Name)
        {
            if (p.getByNAme(Name) == null)
            {
                return BadRequest("There is no category with this name");
            }
            return Ok(p.getBycatname(Name));

        }

        [HttpGet("/catid/{id:int}")]
        public IActionResult getBycatid(int id)
        {
            if (p.getBycatid(id) == null)
            {
                return BadRequest("There is no category with this id");
            }
            return Ok(p.getBycatid(id));

        }

        [HttpGet("{prod:int}")]
        public IActionResult getByprodbyid(int id)
        {
            if (p.getByprodbyid(id) == null)
            {
                return BadRequest("There is no prod with this id");
            }
            return Ok(p.getByprodbyid(id));

        }




    }
}
