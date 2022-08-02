using Lab.Models;
using Lab.Repositories;
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
    public class ShoppingCartItemController : ControllerBase
    {

        IshoppingcartitemRepository shopingcartrepo;
        public ShoppingCartItemController(IshoppingcartitemRepository shopping)
        {
            shopingcartrepo = shopping;

        }
        [HttpGet]
        public IActionResult getAll()
        {
            List<Shopping_cart_item> carts = shopingcartrepo.GetAll();
            return Ok(carts);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Shopping_cart_item cat)
        {
            int x = shopingcartrepo.Update(id, cat);
            return Ok(x);

        }

        [HttpPost]
        public IActionResult Insert(Shopping_cart_item item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    shopingcartrepo.Insert(item);
                    string url = Url.Link("", new { id = item.ID });
                    return Created(url, item);// Created(url, dep);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{itemid:int}")]
        public IActionResult Delete(int itemid)
        {
            
            int x = shopingcartrepo.Delete(itemid);
            return Ok(x);

        }
      
    }
}

