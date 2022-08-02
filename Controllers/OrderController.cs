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
    public class OrderController : ControllerBase
    {

        IOrderRepository Ip;

        public OrderController(IOrderRepository Ip)
        {
            this.Ip = Ip;

        }
        [HttpPost]
        public IActionResult create(Order o)
        {
            

                Ip.Insert(o);
                
                return Ok("addedd");// Created(url, dep);

            
           

        }

    }
}
